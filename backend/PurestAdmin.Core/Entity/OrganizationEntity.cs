// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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