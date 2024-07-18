// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class WfWorkflowEntity
{
    /// <summary>
    /// 流程步骤
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(WfExecutionPointerEntity.WorkflowId), nameof(PersistenceId))]
    public List<WfExecutionPointerEntity> ExecutionPointers { get; set; }
    /// <summary>
    /// 流程定义
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(WorkflowDefinitionId), nameof(WfDefinitionEntity.DefinitionId))]
    public WfDefinitionEntity Definition { get; set; }
    /// <summary>
    /// 创建人名称
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string CreateByName { get; set; }
}
