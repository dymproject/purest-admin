// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.DataEncryption.Extensions;

using PurestAdmin.Application.Auth.Dtos;
using PurestAdmin.Application.Organization.Dtos;
using PurestAdmin.Core.Const;
using PurestAdmin.Core.Multiplex;

namespace PurestAdmin.Application.Auth.Services;
/// <summary>
/// 用户授权服务
/// </summary>
public class AuthService : IAuthService, ITransient
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db;
    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// 用户管理
    /// </summary>
    private readonly IUserManager _userManager;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="userManager"></param>
    public AuthService(ISqlSugarClient db, IHttpContextAccessor httpContextAccessor, IUserManager userManager)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>    
    public async Task<LoginOutput> LoginAsync([Required] LoginInput input)
    {
        // 判断用户名或密码是否正确
        var user = await _db.Queryable<UserEntity>().FirstAsync(u => u.Account.Equals(input.Account)) ?? throw Oops.Bah("用户名不存在或用户名密码错误！");
        if (user.Password.ToAESDecrypt(AesKeyConst.Key) != input.Password.Trim()) throw Oops.Bah("用户名不存在或用户名密码错误！");
        // 映射结果
        var output = user.Adapt<LoginOutput>();

        // 生成 token
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
        {
            { ClaimConst.USERID,user.Id },
            { ClaimConst.ACCOUNT,user.Account },
            { ClaimConst.NAME,user.Name },
            { ClaimConst.ORGANIZATION_ID,user.OrganizationId }
        });

        // 设置 Swagger 自动登录
        _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

        //头返回过期时间以及刷新token
        _httpContextAccessor.HttpContext.Response.Headers["x-access-token"] = JWTEncryption.GenerateRefreshToken(accessToken);

        //功能权限
        var functions = await _userManager.GetFunctionsAsync(user.Id);
        output.Permissions = functions.Select(x => x.Code).ToList();

        return output;
    }

    /// <summary>
    /// 获取当前用户权限信息
    /// </summary>
    /// <returns></returns>
    public async Task<UserInfoOutput> GetUserInfoAsync()
    {
        var claimUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.USERID) ?? throw Oops.Oh("未找到有效的用户信息，请重新登录").StatusCode(401);
        var userId = long.Parse(claimUserId.Value);

        _ = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == userId) ?? throw Oops.Oh("未找到有效的用户信息，请重新登录").StatusCode(401);

        //var securities = await _db.Queryable<SecurityEntity>()
        //    .InnerJoin<RoleSecurityEntity>((s, rs) => s.Id == rs.SecurityId)
        //    .InnerJoin<UserRoleEntity>((s, rs, ur) => rs.RoleId == ur.RoleId)
        //    .Where((s, rs, ur) => ur.UserId == userId)
        //    .Select(s => s).ToListAsync();

        var result = new UserInfoOutput
        {
            //Permissions = securities.Select(x => x.SecurityCode).ToList(),
        };
        return result;
    }

    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    public async Task<List<OrganizationProfile>> GetOrganizationTreeAsync()
    {
        var claimOrganizationId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.ORGANIZATION_ID) ?? throw Oops.Oh("未找到有效的用户信息，请重新登录").StatusCode(401);
        var organizationId = long.Parse(claimOrganizationId.Value);

        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync(x => x.Id == organizationId) ?? throw Oops.Oh("无法找到当前登录用户的组织机构，请联系管理检查数据").StatusCode(401);

        var organizationChildren = await _db.Queryable<OrganizationEntity>().OrderByDescending(x => x.Sort).ToTreeAsync(x => x.Children, x => x.ParentId, organizationId);

        if (organizationChildren != null)
        {
            organization.Children = organizationChildren;
        }
        var result = new List<OrganizationEntity>() { organization };
        return result.Adapt<List<OrganizationProfile>>();
    }
}
