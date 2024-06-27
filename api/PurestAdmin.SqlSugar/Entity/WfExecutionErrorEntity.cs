// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 执行异常
/// </summary>
[SugarTable("PUREST_WF_EXECUTION_ERROR")]
public partial class WfExecutionErrorEntity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
    public long PersistenceId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "WORKFLOW_ID")]
    public string WorkflowId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTION_POINTER_ID")]
    public string ExecutionPointerId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "ERROR_TIME")]
    public DateTime ErrorTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "MESSAGE")]
    public string Message { get; set; }
}