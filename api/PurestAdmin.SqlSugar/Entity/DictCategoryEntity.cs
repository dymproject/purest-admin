// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 字典分类
/// </summary>
[SugarTable("PUREST_DICT_CATEGORY")]
public partial class DictCategoryEntity : BaseEntity
{
    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    [SugarColumn(ColumnName = "CODE")]
    public string Code { get; set; }
}