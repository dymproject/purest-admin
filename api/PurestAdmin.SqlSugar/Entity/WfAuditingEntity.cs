namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 流程审批表
/// </summary>
[SugarTable("PUREST_WF_AUDITING")]
public partial class WfAuditingEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
    public long Id { get; set; }
    /// <summary>
    /// 流程Id
    /// </summary>
    [SugarColumn(ColumnName = "WORKFLOW_ID")]
    public string WorkflowId { get; set; }
    /// <summary>
    /// 步骤Id
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTION_POINTER_ID")]
    public string ExecutionPointerId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "STATUS")]
    public int Status { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "CREATE_TIME")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 审核时间
    /// </summary>
    [SugarColumn(ColumnName = "AUDITING_TIME")]
    public DateTime? AuditingTime { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    [SugarColumn(ColumnName = "AUDITING_OPINION")]
    public string AuditingOpinion { get; set; }
    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "AUDITOR")]
    public long Auditor { get; set; }
    /// <summary>
    /// 审核人类型(用户,角色,组织机构)
    /// </summary>
    [SugarColumn(ColumnName = "AUDITOR_TYPE")]
    public int AuditorType { get; set; }
    /// <summary>
    /// 是否会签
    /// </summary>
    [SugarColumn(ColumnName = "IS_COUNTERSIGN")]
    public bool IsCountersign { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "REMARK")]
    public string Remark { get; set; }
}