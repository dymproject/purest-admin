// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

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