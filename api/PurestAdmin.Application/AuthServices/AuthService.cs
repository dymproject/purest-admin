// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using PurestAdmin.Application.AuthServices.Dtos;
using PurestAdmin.Core.DataEncryption.Encryptions;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Application.AuthServices;
/// <summary>
/// 用户授权服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class AuthService(IAdminToken adminToken, IHttpContextAccessor httpContextAccessor, ISqlSugarClient db, ICurrentUser currentUser) : ApplicationService
{
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
            new Claim(AdminClaimConst.ROLE_ID,userRole.RoleId.ToString()),
        };

        var accessToken = _adminToken.GenerateTokenString(claims);

        // 返回accesstoken
        _httpContextAccessor.HttpContext.Response.Headers["accesstoken"] = accessToken;

        return output;
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
}
