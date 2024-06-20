// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class WfWorkflowEntity
{
    [Navigate(NavigateType.OneToMany, nameof(WfPointerEntity.WorkflowId))]
    public List<WfPointerEntity> ExecutionPointers { get; set; } = [];
}
