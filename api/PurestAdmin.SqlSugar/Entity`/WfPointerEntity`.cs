// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class WfPointerEntity
{
    [Navigate(NavigateType.OneToMany, nameof(WfAttributeEntity.ExecutionPointerId))]
    public List<WfAttributeEntity> ExtensionAttributes { get; set; } = [];
}
