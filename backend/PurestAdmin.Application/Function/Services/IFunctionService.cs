// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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