// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.DictCategory.Dtos;

/// <summary>
/// 字典分类添加
/// </summary>
public class AddDictCategoryInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 分类名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "分类名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    [MaxLength(20, ErrorMessage = "分类编码最大长度为：20")]
    public string Code { get; set; }
}