// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
