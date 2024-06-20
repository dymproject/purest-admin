namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 订阅
/// </summary>
[SugarTable("PUREST_WF_SUBSCRIPTION")]
public partial class WfSubscriptionEntity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
    public long PersistenceId { get; set; }
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
    [SugarColumn(ColumnName = "STEP_ID")]
    public int StepId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "SUBSCRIPTION_ID")]
    public string SubscriptionId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "WORKFLOW_ID")]
    public string WorkflowId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "SUBSCRIBE_AS_OF")]
    public DateTime SubscribeAsOf { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "SUBSCRIPTION_DATA")]
    public string SubscriptionData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTION_POINTER_ID")]
    public string ExecutionPointerId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXTERNAL_TOKEN")]
    public string ExternalToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXTERNAL_TOKEN_EXPIRY")]
    public DateTime? ExternalTokenExpiry { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXTERNAL_WORKER_ID")]
    public string ExternalWorkerId { get; set; }
}