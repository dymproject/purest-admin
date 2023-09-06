// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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