// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Role.Dtos;

namespace PurestAdmin.Application.Role.Services;
/// <summary>
/// 角色接口
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<RoleProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<RoleProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddRoleInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddRoleInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
    /// <summary>
    /// 赋给功能权限
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task AssignFunctionAsync(long roleId, long[] input);
    /// <summary>
    /// 全量查询
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<List<RoleProfile>> GetAllListAsync(string roleName);
    /// <summary>
    /// 获取角色的功能
    /// </summary>
    Task<List<Function.Dtos.FunctionProfile>> GetFunctionsAsync(long roleId);
}