// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
/// <summary>
/// 通知公告表
/// </summary>
[SugarTable("PUREST_NOTICE")]
public partial class NoticeEntity : BaseEntity
{
    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn(ColumnName = "TITLE")]
    public string Title { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnName = "CONTENT")]
    public string Content { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "NOTICE_TYPE")]
    public long NoticeType { get; set; }
    /// <summary>
    /// 级别
    /// </summary>
    [SugarColumn(ColumnName = "LEVEL")]
    public long Level { get; set; }
}
