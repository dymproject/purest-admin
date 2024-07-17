namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 流程审批记录
/// </summary>
[SugarTable("PUREST_WF_AUDITING_RECORD")]
public partial class WfAuditingRecordEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
    public long Id { get; set; }
    /// <summary>
    /// 步骤Id
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTION_POINTER_ID")]
    public long ExecutionPointerId { get; set; }
    /// <summary>
    /// 审批时间
    /// </summary>
    [SugarColumn(ColumnName = "AUDITING_TIME")]
    public DateTime AuditingTime { get; set; }
    /// <summary>
    /// 审批人
    /// </summary>
    [SugarColumn(ColumnName = "AUDITOR")]
    public long Auditor { get; set; }
    /// <summary>
    /// 审批人姓名
    /// </summary>
    [SugarColumn(ColumnName = "AUDITOR_NAME")]
    public string AuditorName { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "AUDITING_OPINION")]
    public string AuditingOpinion { get; set; }
    /// <summary>
    /// 是否同意
    /// </summary>
    [SugarColumn(ColumnName = "IS_AGREE")]
    public bool IsAgree { get; set; }
}