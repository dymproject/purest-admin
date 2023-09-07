// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 组织机构
/// </summary>
[SugarTable("PUREST_ORGANIZATION")]
public partial class OrganizationEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    [SugarColumn(ColumnName = "PARENT_ID")]
    public long? ParentId { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "TELEPHONE")]
    public string Telephone { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "LEADER")]
    public string Leader { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "SORT")]
    public int? Sort { get; set; }
}