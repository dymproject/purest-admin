// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 流程定义
/// </summary>
[SugarTable("PUREST_WF_DEFINITION")]
public partial class WfDefinitionEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 定义ID
    /// </summary>
    [SugarColumn(ColumnName = "DEFINITION_ID")]
    public string DefinitionId { get; set; }
    /// <summary>
    /// 流程内容
    /// </summary>
    [SugarColumn(ColumnName = "WORKFLOW_CONTENT")]
    public string WorkflowContent { get; set; }
    /// <summary>
    /// 设计器内容
    /// </summary>
    [SugarColumn(ColumnName = "DESIGNS_CONTENT")]
    public string DesignsContent { get; set; }
    /// <summary>
    /// 表单内容
    /// </summary>
    [SugarColumn(ColumnName = "FORM_CONTENT")]
    public string FormContent { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "VERSION")]
    public int Version { get; set; }
    /// <summary>
    /// 是否锁定
    /// </summary>
    [SugarColumn(ColumnName = "IS_LOCKED")]
    public bool IsLocked { get; set; }
}