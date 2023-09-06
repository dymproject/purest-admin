// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Interface.Dtos;

namespace PurestAdmin.Application.Interface.Services;
/// <summary>
/// 接口功能接口
/// </summary>
public interface IInterfaceService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<InterfaceGroupProfile>> GetPagedListAsync(GetPagedListInput input);

    /// <summary>
    /// 导入接口
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    Task<int> ImportInterfaceAsync(IFormFile file);
}
