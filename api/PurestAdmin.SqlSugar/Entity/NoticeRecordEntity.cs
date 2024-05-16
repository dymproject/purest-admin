// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
/// <summary>
/// 通知公告表
/// </summary>
[SugarTable("PUREST_NOTICE_RECORD")]
public partial class NoticeRecordEntity : BaseEntity
{
    /// <summary>
    /// 通知公告Id
    /// </summary>
    [SugarColumn(ColumnName = "NOTICE_ID")]
    public long NoticeId { get; set; }
    /// <summary>
    /// 接收人
    /// </summary>
    [SugarColumn(ColumnName = "RECEIVER")]
    public long Receiver { get; set; }
    /// <summary>
    /// 是否已读
    /// </summary>
    [SugarColumn(ColumnName = "IS_READ")]
    public bool IsRead { get; set; }
}
