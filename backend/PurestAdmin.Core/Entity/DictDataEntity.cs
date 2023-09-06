// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 字典数据
/// </summary>
[SugarTable("PUREST_DICT_DATA")]
public partial class DictDataEntity : BaseEntity
{
    /// <summary>
    /// 字典分类ID
    /// </summary>
    [SugarColumn(ColumnName = "CATEGORY_ID")]
    public long CategoryId { get; set; }
    /// <summary>
    /// 字典名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "SORT")]
    public int Sort { get; set; }
}