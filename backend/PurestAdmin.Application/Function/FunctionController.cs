// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Function.Dtos;
using PurestAdmin.Application.Function.Services;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 功能接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class FunctionController : IDynamicApiController
{
    private readonly IFunctionService _functionService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="functionService"></param>
    public FunctionController(IFunctionService functionService)
    {
        _functionService = functionService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<FunctionProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _functionService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<FunctionProfile> GetAsync(long id)
    {
        return await _functionService.GetAsync(id);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<long> AddAsync(AddFunctionInput input)
    {
        return await _functionService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task PutAsync(long id, AddFunctionInput input)
    {
        await _functionService.PutAsync(id, input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        await _functionService.DeleteAsync(id);
    }

    /// <summary>
    /// 树形查询
    /// </summary>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "tree")]
    public async Task<List<FunctionProfile>> GetTreeAsync()
    {
        return await _functionService.GetFunctionTreeAsync();
    }

    /// <summary>
    /// 获取功能绑定的接口
    /// </summary>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "interfaces")]
    public async Task<List<BindedInterfaceProfile>> GetInterfaceAsync([Required, ApiSeat(ApiSeats.ActionStart)] long functionId)
    {
        return await _functionService.GetInterfacesAsync(functionId);
    }

    /// <summary>
    /// 获取功能所属的接口
    /// </summary>
    /// <param name="functionId"></param>
    /// <param name="interfaceId"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork, ApiDescriptionSettings(Name = "assign-interface")]
    public async Task AssignInterfaceAsync([Required, ApiSeat(ApiSeats.ActionStart)] long functionId, [FromBody] long interfaceId)
    {
        await _functionService.AssignInterfaceAsync(functionId, interfaceId);
    }

    /// <summary>
    /// 移除此功能的对应接口
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, UnitOfWork, ApiDescriptionSettings(Name = "interface")]
    public async Task RemoveInterfaceAsync([Required, ApiSeat(ApiSeats.ActionStart)] long id)
    {
        await _functionService.RemoveInterfaceAsync(id);
    }
}

