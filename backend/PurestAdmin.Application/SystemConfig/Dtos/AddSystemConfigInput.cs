// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.SystemConfig.Dtos;

/// <summary>
/// 系统配置表添加
/// </summary>
public class AddSystemConfigInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(40, ErrorMessage = "编码最大长度为：40")]
    public string ConfigCode { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    [MaxLength(1000, ErrorMessage = "值最大长度为：1000")]
    public string ConfigValue { get; set; }
}