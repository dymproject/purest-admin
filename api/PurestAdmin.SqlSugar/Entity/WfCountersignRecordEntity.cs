namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 会签记录
/// </summary>
[SugarTable("PUREST_WF_COUNTERSIGN_RECORD")]
public partial class WfCountersignRecordEntity
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
	/// 审核人
	/// </summary>
	[SugarColumn(ColumnName = "AUDITOR")]
	public long Auditor { get; set; }
	/// <summary>
	/// 审核时间
	/// </summary>
	[SugarColumn(ColumnName = "AUDITING_TIME")]
	public DateTime AuditingTime { get; set; }
	/// <summary>
	/// 审核意见
	/// </summary>
	[SugarColumn(ColumnName = "AUDITING_OPINION")]
	public string AuditingOpinion { get; set; }
}