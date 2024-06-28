// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.DataTypes;
public class GeneralAuditingDefinition
{
    public Dictionary<string, object> Storage { get; set; } = [];

    public object this[string propertyName]
    {
        get => Storage.TryGetValue(propertyName, out var value) ? value : null;
        set => Storage[propertyName] = value;
    }
}
