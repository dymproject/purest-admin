// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Function.Dtos;

namespace PurestAdmin.Application.Function.Services;
/// <summary>
/// 功能接口
/// </summary>
public interface IFunctionService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<FunctionProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<FunctionProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddFunctionInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddFunctionInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
    /// <summary>
    /// 树查询
    /// </summary>
    Task<List<FunctionProfile>> GetFunctionTreeAsync();
    /// <summary>
    /// 获取功能拥有的接口
    /// </summary>
    /// <param name="functionId"></param>
    /// <returns></returns>
    Task<List<BindedInterfaceProfile>> GetInterfacesAsync(long functionId);
    /// <summary>
    /// 给功能分配接口
    /// </summary>
    /// <param name="functionId"></param>
    /// <param name="interfaceId"></param>
    Task AssignInterfaceAsync(long functionId, long interfaceId);
    /// <summary>
    /// 移除功能的接口
    /// </summary>
    /// <param name="id"></param>
    Task RemoveInterfaceAsync(long id);
}