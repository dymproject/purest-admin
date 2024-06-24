namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 自定义属性
/// </summary>
[SugarTable("PUREST_WF_EXECUTION_ATTRIBUTE")]
public partial class WfExecutionAttributeEntity
{
	/// <summary>
	/// 
	/// </summary>
	[SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
	public long PersistenceId { get; set; }
	/// <summary>
	/// 
	/// </summary>
	[SugarColumn(ColumnName = "ATTRIBUTE_KEY")]
	public string AttributeKey { get; set; }
	/// <summary>
	/// 
	/// </summary>
	[SugarColumn(ColumnName = "ATTRIBUTE_VALUE")]
	public string AttributeValue { get; set; }
	/// <summary>
	/// 
	/// </summary>
	[SugarColumn(ColumnName = "EXECUTION_POINTER_ID")]
	public long ExecutionPointerId { get; set; }
}