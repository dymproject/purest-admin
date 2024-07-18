// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class WfExecutionPointerEntity
{
    [Navigate(NavigateType.OneToMany, nameof(WfExecutionAttributeEntity.ExecutionPointerId), nameof(PersistenceId))]
    public List<WfExecutionAttributeEntity> ExtensionAttributes { get; set; }

    [Navigate(NavigateType.OneToMany, nameof(WfAuditingRecordEntity.ExecutionPointerId), nameof(PersistenceId))]
    public List<WfAuditingRecordEntity> AuditingRecords { get; set; }
}
