// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 计划命令
/// </summary>
[SugarTable("PUREST_WF_SCHEDULED_COMMAND")]
public partial class WfScheduledCommandEntity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
    public long PersistenceId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "COMMAND_NAME")]
    public string CommandName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "DATA")]
    public string Data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTE_TIME")]
    public long ExecuteTime { get; set; }
}