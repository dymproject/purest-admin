// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Security.Claims;
using System.Text.Json;

using Flurl;
using Flurl.Http;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

using PurestAdmin.Application.AuthServices.Dtos;
using PurestAdmin.Core.DataEncryption.Encryptions;
using PurestAdmin.Core.DataEncryption.Extensions;
using PurestAdmin.Multiplex.AdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;
using PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;

namespace PurestAdmin.Application.AuthServices;
/// <summary>
/// 用户授权服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class AuthService(IOAuth2UserManager oAuth2UserManager, IHubContext<AuthorizationHub, IAuthorizationClient> hubContext, IConfiguration configuration, IAdminToken adminToken, IHttpContextAccessor httpContextAccessor, ISqlSugarClient db, ICurrentUser currentUser) : ApplicationService
{
    /// <summary>
    /// oAuth2UserManager
    /// </summary>
    private readonly IOAuth2UserManager _oAuth2UserManager = oAuth2UserManager;
    /// <summary>
    /// hubContext
    /// </summary>
    private readonly IHubContext<AuthorizationHub, IAuthorizationClient> _hubContext = hubContext;
    /// <summary>
    /// configuration
    /// </summary>
    private readonly IConfiguration _configuration = configuration;
    /// <summary>
    /// IAdminToken
    /// </summary>
    private readonly IAdminToken _adminToken = adminToken;
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    /// <summary>
    /// 当前用户
    /// </summary>
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<LoginOutput> LoginAsync([Required] LoginInput input)
    {
        // 判断用户名或密码是否正确
        var password = MD5Encryption.Encrypt(input.Password);
        var user = await _db.Queryable<UserEntity>().FirstAsync(u => u.Account.Equals(input.Account) && u.Password.Equals(password)) ?? throw PersistdValidateException.Message("用户名不存在或用户名密码错误！");
        if (user.Status != (int)UserStatusEnum.Normal) throw PersistdValidateException.Message("帐号状态异常，请联系管理员");
        var userRole = await _db.Queryable<UserRoleEntity>().FirstAsync(x => x.UserId == user.Id);
        // 映射结果
        var output = user.Adapt<LoginOutput>();

        //Payload,存放用户信息
        var claims = new[]
        {
            new Claim(AdminClaimConst.USER_ID,user.Id.ToString()),
            new Claim(AdminClaimConst.USER_NAME,user.Name),
            new Claim(AdminClaimConst.ORGANIZATION_ID,user.OrganizationId.ToString()),
        };

        var accessToken = _adminToken.GenerateTokenString(claims);

        // 返回accesstoken
        _httpContextAccessor.HttpContext.Response.Headers["accesstoken"] = accessToken;
        return output;
    }

    /// <summary>
    /// Auht2.0 回调服务
    /// </summary>
    /// <param name="input"></param>
    [AllowAnonymous]
    public async Task GetCallbackAsync(GetCallbackInput input)
    {
        var stateInfoJson = input.State.ToAESDecrypt("BB5F6811B9056BE16F22A793642ED588");
        var stateInfo = JsonSerializer.Deserialize<StateInfo>(stateInfoJson) ?? throw BusinessValidateException.Message("回调参数State异常");
        var authorizationCenters = _configuration.GetRequiredSection("OAuth2Options").Get<List<OAuth2Option>>() ?? throw BusinessValidateException.Message("未配置认证中心");
        var authorizationCenter = authorizationCenters?.FirstOrDefault(x => string.Equals(x.Name, stateInfo.Type, StringComparison.OrdinalIgnoreCase))
            ?? throw BusinessValidateException.Message("未找到当前认证配置");
        OAuth2UserInfo oAuth2UserInfo = new();
        try
        {
            switch (stateInfo.Type)
            {
                case OAuth2TypeConst.GITEE:
                    var tokenResult = await "https://gitee.com/oauth/token".SetQueryParams(new { grant_type = "authorization_code", code = input.Code, client_id = authorizationCenter.ClientId, redirect_uri = authorizationCenter.RedirectUri, client_secret = authorizationCenter.ClientSecret }).PostAsync().ReceiveJson<GiteeTokenResult>();
                    oAuth2UserInfo = await "https://gitee.com/api/v5/user".SetQueryParams(new { access_token = tokenResult.AccessToken }).GetJsonAsync<OAuth2UserInfo>();
                    oAuth2UserInfo.Type = OAuth2TypeConst.GITEE;
                    break;
                default:
                    break;
            }
            var oAuth2User = await _oAuth2UserManager.GetOAuth2UserPersistenceIdAsync(oAuth2UserInfo);
            if (oAuth2User.UserId.HasValue)
            {
                var (accessToken, userInfo) = await GetTokenAndUserInfoAsync(oAuth2User.UserId.Value);
                await _hubContext.Clients.Client(stateInfo.ConnectionId).NoticeRedirect(accessToken, userInfo);
            }
            else
                await _hubContext.Clients.Client(stateInfo.ConnectionId).NoticeRegister(oAuth2User.PersistenceId);
        }
        catch (FlurlHttpException ex)
        {
            //这里基本都是因为访问权限的问题，酌情处理
            await _hubContext.Clients.Client(stateInfo.ConnectionId).NoticeException(ex.Message);
        }
    }

    /// <summary>
    /// 绑定用户
    /// </summary>
    /// <param name="input">input</param>
    [AllowAnonymous]
    public async Task BindUserAsync([Required] BindUserInput input)
    {
        var userOutput = await LoginAsync(input);
        var oAuth2User = await _db.Queryable<OAuth2UserEntity>().FirstAsync(x => x.PersistenceId == input.OAuth2UserId);
        if (oAuth2User != null)
        {
            oAuth2User.UserId = userOutput.Id;
            await _db.Updateable(oAuth2User).ExecuteCommandAsync();
            var (accessToken, userInfo) = await GetTokenAndUserInfoAsync(oAuth2User.UserId.Value);
            await _hubContext.Clients.Client(input.ConnectionId).NoticeRedirect(accessToken, userInfo);
        }
    }

    /// <summary>
    /// 注册用户
    /// </summary>
    /// <param name="input">input</param>
    [AllowAnonymous, UnitOfWork]
    public async Task RegisterUserAsync([Required] RegisterUserInput input)
    {
        var entity = input.Adapt<UserEntity>();
        //这个地方要实现给用户增加一个默认的组织机构和角色，可根据自己业务进行调整，我们就默认系统第一个吧
        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync();

        entity.OrganizationId = organization.Id;
        var userId = await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
        //给用户绑定角色
        var role = await _db.Queryable<RoleEntity>().FirstAsync();
        await _db.Insertable(new UserRoleEntity() { RoleId = role.Id, UserId = userId }).ExecuteReturnSnowflakeIdAsync();

        var oAuth2User = await _db.Queryable<OAuth2UserEntity>().FirstAsync(x => x.PersistenceId == input.OAuth2UserId) ?? throw BusinessValidateException.Message("未找到认证中心的用户数据");
        if (oAuth2User != null)
        {
            oAuth2User.UserId = userId;
            await _db.Updateable(oAuth2User).ExecuteCommandAsync();
            var (accessToken, userInfo) = await GetTokenAndUserInfoAsync(oAuth2User.UserId.Value);
            await _hubContext.Clients.Client(input.ConnectionId).NoticeRedirect(accessToken, userInfo);
        }
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns></returns>
    public async Task<GetUserInfoOutput> GetUserInfoAsync([Required(ErrorMessage = "请输入密码")] string password)
    {
        var claimUserId = _currentUser.Id;
        var user = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == _currentUser.Id);
        if (!user.Password.Equals(MD5Encryption.Encrypt(password))) throw PersistdValidateException.Message("密码错误！");
        return user.Adapt<GetUserInfoOutput>();
    }

    /// <summary>
    /// 获取当前用户的功能
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetFunctionsAsync()
    {
        var functions = await _currentUser.GetFunctionsAsync();
        return functions.Select(x => x.Code).ToList();
    }

    /// <summary>
    /// 修改当前用户信息
    /// </summary>
    /// <returns></returns>
    public async Task PutUserInfoAsync(PutUserInfoInput input)
    {
        var claimUserId = _currentUser.Id;
        var user = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == _currentUser.Id);
        user = input.Adapt(user);
        user.Password = MD5Encryption.Encrypt(input.Password);
        await _db.Updateable(user).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetOrganizationTreeOutput>> GetOrganizationTreeAsync()
    {
        var organizationId = _currentUser.OrganizationId;

        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync(x => x.Id == organizationId) ?? throw PersistdValidateException.Message("无法找到当前登录用户的组织机构，请联系管理检查数据");

        var organizationChildren = await _db.Queryable<OrganizationEntity>().OrderByDescending(x => x.Sort).ToTreeAsync(x => x.Children, x => x.ParentId, organizationId);

        if (organizationChildren != null)
        {
            organization.Children = organizationChildren;
        }
        var result = new List<OrganizationEntity>() { organization };
        return result.Adapt<List<GetOrganizationTreeOutput>>();
    }

    /// <summary>
    /// 获得当前平台信息
    /// </summary>
    /// <returns></returns>
    public GetSystemPlatformInfoOutput GetSystemPlatformInfoAsync()
    {
        return new GetSystemPlatformInfoOutput();
    }

    /// <summary>
    /// 获得用户的通知
    /// </summary>
    /// <returns></returns>
    public async Task<List<NoticeItemModel>> GetUnreadNoticeAsync()
    {
        var records = await _db.Queryable<NoticeRecordEntity>()
            .LeftJoin<NoticeEntity>((r, n) => r.NoticeId == n.Id)
            .LeftJoin<DictDataEntity>((r, n, d) => n.Level == d.Id)
            .Where(r => r.Receiver == _currentUser.Id && !r.IsRead && r.CreateTime <= Clock.Now.AddDays(3))
            .Select((r, n, d) => new NoticeItemModel
            {
                DateTime = r.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Description = n.Content,
                Extra = d.Name,
                Status = d.Remark,
                Title = n.Title,
                Type = n.NoticeType.ToString()
            }).ToListAsync();
        return records;
    }

    #region 私有方法
    /// <summary>
    /// 获取TokenAndUserInfo
    /// </summary>
    /// <param name="oAuth2UserId"></param>
    /// <returns></returns>
    private async Task<(string, dynamic)> GetTokenAndUserInfoAsync(long oAuth2UserId)
    {
        var user = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == oAuth2UserId);
        var userRole = await _db.Queryable<UserRoleEntity>().FirstAsync(x => x.UserId == user.Id);
        var claims = new[]
        {
            new Claim(AdminClaimConst.USER_ID,user.Id.ToString()),
            new Claim(AdminClaimConst.USER_NAME,user.Name),
            new Claim(AdminClaimConst.ORGANIZATION_ID,user.OrganizationId.ToString()),
        };
        var accessToken = _adminToken.GenerateTokenString(claims);
        var functions = await _db.Queryable<RoleFunctionEntity>()
            .LeftJoin<FunctionEntity>((rf, f) => rf.FunctionId == f.Id)
            .Where((rf, f) => rf.RoleId == SqlFunc.Subqueryable<UserRoleEntity>().Where(u => u.UserId == user.Id).Select(u => u.RoleId))
            .Select((rf, f) => f)
            .ToListAsync();
        var userInfo = new { user.Name, user.Id, Permissions = functions.Select(x => x.Code).ToList() };
        return (accessToken, userInfo);
    }
    #endregion
}
