// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Core.Cache;
using PurestAdmin.Core.ExceptionExtensions;
using PurestAdmin.Multiplex.Contracts.Consts;
using PurestAdmin.Multiplex.Contracts.IAdminUser;

namespace PurestAdmin.Multiplex.AdminUser;
/// <summary>
/// 当前用户
/// </summary>
public class CurrentUser(ISqlSugarClient db, IHttpContextAccessor httpContextAccessor, IAdminCache cache) : ICurrentUser, ITransientDependency
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;

    /// <summary>
    /// HttpContext 访问器
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// 缓存
    /// </summary>
    private readonly IAdminCache _cache = cache;

    /// <summary>
    /// 用户Id
    /// </summary>
    public long Id
    {
        get => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(AdminClaimConst.USER_ID)?.Value ?? throw PersistdValidateException.Message("令牌超时，请重新登录！"));
    }
    /// <summary>
    /// 角色Id
    /// </summary>
    public long RoleId
    {
        get => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(AdminClaimConst.ROLE_ID)?.Value ?? throw PersistdValidateException.Message("令牌超时，请重新登录！"));
    }
    /// <summary>
    /// 组织机构Id
    /// </summary>
    public long OrganizationId
    {
        get => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(AdminClaimConst.ORGANIZATION_ID)?.Value ?? throw PersistdValidateException.Message("令牌超时，请重新登录！"));
    }
    /// <summary>
    /// self
    /// </summary>
    public UserEntity Self
    {
        get => _db.Queryable<UserEntity>().First(x => x.Id == Id) ?? throw PersistdValidateException.Message("用户不存在！");
    }

    /// <summary>
    /// 用户拥有的功能
    /// </summary>
    /// <returns></returns>
    public async Task<List<FunctionEntity>> GetFunctionsAsync()
    {
        return await _db.Queryable<RoleFunctionEntity>()
          .LeftJoin<FunctionEntity>((rf, f) => rf.FunctionId == f.Id)
          .Where((rf, f) => rf.RoleId == SqlFunc.Subqueryable<UserRoleEntity>().Where(u => u.UserId == Id).Select(u => u.RoleId))
          .Select((rf, f) => f)
          .ToListAsync();
    }

    /// <summary>
    /// 用户拥有的接口
    /// </summary>
    /// <returns></returns>
    public async Task<List<InterfaceEntity>> GetInterfacesAsync()
    {
        var interfaces = await _cache.Get(AdminClaimConst.CACHE_ROLESINTERFACE_PREFIX + RoleId, async () =>
        {
            return await _db.Queryable<UserRoleEntity>()
            .RightJoin<RoleFunctionEntity>((ur, rf) => ur.RoleId == rf.RoleId)
            .RightJoin<FunctionInterfaceEntity>((ur, rf, fi) => rf.FunctionId == fi.FunctionId)
            .RightJoin<InterfaceEntity>((ur, rf, fi, inter) => fi.InterfaceId == inter.Id)
            .Where((ur, rf, fi, inter) => ur.UserId == Id)
            .Select((ur, rf, fi, inter) => inter)
            .ToListAsync();
        });
        return interfaces ?? [];
    }

    /// <summary>
    /// 用户的组织机构树
    /// </summary>
    /// <returns></returns>
    public async Task<List<OrganizationEntity>> GetOrganizationTreeAsync()
    {
        var organizationId = OrganizationId;

        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync(x => x.Id == organizationId) ?? throw PersistdValidateException.Message("无法找到当前登录用户的组织机构，请联系管理检查数据");

        var organizationChildren = await _db.Queryable<OrganizationEntity>().OrderByDescending(x => x.Sort).ToTreeAsync(x => x.Children, x => x.ParentId, organizationId);

        if (organizationChildren != null)
        {
            organization.Children = organizationChildren;
        }
        return [organization];
    }
}
