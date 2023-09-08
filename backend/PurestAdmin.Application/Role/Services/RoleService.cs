// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Role.Dtos;

namespace PurestAdmin.Application.Role.Services;
/// <summary>
/// 角色服务
/// </summary>
public class RoleService : IRoleService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<RoleEntity> _roleRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="roleRepository"></param>
    public RoleService(ISqlSugarClient db, Repository<RoleEntity> roleRepository)
    {
        _db = db;
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<RoleProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<RoleEntity>()
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name))
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<RoleProfile>>();
    }

    /// <summary>
    /// 全量查询
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<List<RoleProfile>> GetAllListAsync(string roleName)
    {
        var list = await _db.Queryable<RoleEntity>()
            .WhereIF(!roleName.IsNullOrEmpty(), x => x.Name.Contains(roleName))
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();
        return list.Adapt<List<RoleProfile>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<RoleProfile> GetAsync(long id)
    {
        var entity = await _roleRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        return entity.Adapt<RoleProfile>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddRoleInput input)
    {
        var entity = input.Adapt<RoleEntity>();
        return await _roleRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddRoleInput input)
    {
        var entity = await _roleRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _roleRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _roleRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        _ = await _roleRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 赋给角色功能
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task AssignFunctionAsync(long roleId, long[] input)
    {
        _ = await _roleRepository.GetByIdAsync(roleId) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);

        _ = await _db.Deleteable<RoleFunctionEntity>().Where(x => x.RoleId == roleId).ExecuteCommandAsync();

        var roleSecurityEntities = input.Select(x => new RoleFunctionEntity { RoleId = roleId, FunctionId = x }).ToList();
        _ = await _db.Insertable(roleSecurityEntities).ExecuteReturnSnowflakeIdListAsync();
    }
    /// <summary>
    /// 获取角色的功能
    /// </summary>
    public async Task<List<Function.Dtos.FunctionProfile>> GetFunctionsAsync(long roleId)
    {
        var functions = await _db.Queryable<RoleFunctionEntity>()
            .LeftJoin<FunctionEntity>((rf, f) => rf.FunctionId == f.Id)
            .Where((rf, f) => rf.RoleId == roleId)
            .Select((rf, f) => f)
            .ToListAsync();
        return functions.Adapt<List<Function.Dtos.FunctionProfile>>();
    }
}
