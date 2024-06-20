namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 事件
/// </summary>
[SugarTable("PUREST_WF_EVENT")]
public partial class WfEventEntity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
    public long PersistenceId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_ID")]
    public string EventId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_NAME")]
    public string EventName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_KEY")]
    public string EventKey { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_DATA")]
    public string EventData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EVENT_TIME")]
    public DateTime EventTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "IS_PROCESSED")]
    public bool IsProcessed { get; set; }
}