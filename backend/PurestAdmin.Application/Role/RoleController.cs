// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。


using PurestAdmin.Application.Role.Dtos;
using PurestAdmin.Application.Role.Services;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 角色接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM, Description = "角色接口")]
public class RoleController : IDynamicApiController
{
    private readonly IRoleService _roleService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleService"></param>
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<RoleProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _roleService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "all")]
    public async Task<List<RoleProfile>> GetAllListAsync([FromQuery] string roleName)
    {
        return await _roleService.GetAllListAsync(roleName);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<RoleProfile> GetAsync(long id)
    {
        return await _roleService.GetAsync(id);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task<long> AddAsync(AddRoleInput input)
    {
        return await _roleService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut, UnitOfWork]
    public async Task PutAsync(long id, [FromBody] AddRoleInput input)
    {
        await _roleService.PutAsync(id, input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, UnitOfWork]
    public async Task DeleteAsync(long id)
    {
        await _roleService.DeleteAsync(id);
    }

    /// <summary>
    /// 赋给角色功能
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork, ApiDescriptionSettings(Name = "assign-function")]
    public async Task AssignFunctionAsync([ApiSeat(ApiSeats.ActionStart)] long roleId, [FromBody] long[] input)
    {
        await _roleService.AssignFunctionAsync(roleId, input);
    }

    /// <summary>
    /// 获取角色的功能
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "functions")]
    public async Task<List<Application.Function.Dtos.FunctionProfile>> AddAsync([Required, ApiSeat(ApiSeats.ActionStart)] long roleId)
    {
        return await _roleService.GetFunctionsAsync(roleId);
    }
}
