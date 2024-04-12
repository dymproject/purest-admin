// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.InterfaceServices.Dtos;
/// <summary>
/// 接口详情
/// </summary>
public class InterfaceOutput
{
    /// <summary>
    /// id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 接口名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 接口地址
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 请求方法
    /// </summary>
    public string RequestMethod { get; set; }
    /// <summary>
    /// 分组Id
    /// </summary>
    public long GroupId { get; set; }
}
