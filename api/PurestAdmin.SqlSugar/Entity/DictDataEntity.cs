// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

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