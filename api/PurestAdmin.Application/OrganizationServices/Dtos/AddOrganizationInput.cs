// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.Organization.Dtos;

/// <summary>
/// 组织机构添加
/// </summary>
public class AddOrganizationInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(100, ErrorMessage = "名称最大长度为：100")]
    public string Name { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    [Required(ErrorMessage = "父级Id不能为空")]
    public long? ParentId { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    [MaxLength(20, ErrorMessage = "联系电话最大长度为：20")]
    public string Telephone { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    [MaxLength(20, ErrorMessage = "负责人最大长度为：20")]
    public string Leader { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int? Sort { get; set; }
}