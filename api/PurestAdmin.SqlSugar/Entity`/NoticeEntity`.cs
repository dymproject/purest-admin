// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class NoticeEntity
{
    /// <summary>
    /// 类型string
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string NoticeTypeString { get; set; }
    /// <summary>
    /// 类型字典
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public DictDataEntity NoticeTypeEntity { get; set; }
    /// <summary>
    /// 级别string
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string LevelString { get; set; }
    /// <summary>
    /// 级别字典
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public DictDataEntity LevelEntity { get; set; }
}
