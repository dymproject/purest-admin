namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 步骤
/// </summary>
[SugarTable("PUREST_WF_EXECUTION_POINTER")]
public partial class WfExecutionPointerEntity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
    public long PersistenceId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "ACTIVE")]
    public bool Active { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "RETRY_COUNT")]
    public int RetryCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "END_TIME")]
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_DATA")]
    public string EventData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_KEY")]
    public string EventKey { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_NAME")]
    public string EventName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_PUBLISHED")]
    public bool EventPublished { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "ID")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_DATA")]
    public string PersistenceData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "SLEEP_UNTIL")]
    public DateTime? SleepUntil { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "START_TIME")]
    public DateTime? StartTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "STEP_ID")]
    public int StepId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "STEP_NAME")]
    public string StepName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "WORKFLOW_ID")]
    public long WorkflowId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "CHILDREN")]
    public string Children { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "CONTEXT_ITEM")]
    public string ContextItem { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PREDECESSOR_ID")]
    public string PredecessorId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "OUTCOME")]
    public string Outcome { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "SCOPE")]
    public string Scope { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "STATUS")]
    public int Status { get; set; }
}