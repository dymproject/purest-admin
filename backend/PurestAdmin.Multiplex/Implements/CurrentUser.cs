// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Core.OopsExtensions;

namespace PurestAdmin.Multiplex.Implements;
/// <summary>
/// 当前用户
/// </summary>
public class CurrentUser(ISqlSugarClient db, IHttpContextAccessor httpContextAccessor) : ICurrentUser, ITransientDependency
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
    /// 获取用户 Id
    /// </summary>
    public long Id
    {
        get => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("userid")?.Value ?? throw Oops.Bah("令牌超时，请重新登录！"));
    }

    public UserEntity Self
    {
        get => _db.Queryable<UserEntity>().First(x => x.Id == Id) ?? throw Oops.Bah("用户不存在！");
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
        return await _db.Queryable<UserRoleEntity>()
            .RightJoin<RoleFunctionEntity>((ur, rf) => ur.RoleId == rf.RoleId)
            .RightJoin<FunctionInterfaceEntity>((ur, rf, fi) => rf.FunctionId == fi.FunctionId)
            .RightJoin<InterfaceEntity>((ur, rf, fi, inter) => fi.InterfaceId == inter.Id)
            .Where((ur, rf, fi, inter) => ur.UserId == Id)
            .Select((ur, rf, fi, inter) => inter)
            .ToListAsync();
    }

    /// <summary>
    /// 用户的组织机构树
    /// </summary>
    /// <returns></returns>
    public async Task<List<OrganizationEntity>> GetOrganizationTreeAsync()
    {
        var organizationId = this.Self.OrganizationId;

        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync(x => x.Id == organizationId) ?? throw Oops.Bah("无法找到当前登录用户的组织机构，请联系管理检查数据");

        var organizationChildren = await _db.Queryable<OrganizationEntity>().OrderByDescending(x => x.Sort).ToTreeAsync(x => x.Children, x => x.ParentId, organizationId);

        if (organizationChildren != null)
        {
            organization.Children = organizationChildren;
        }
        return [organization];
    }
}
