// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;

using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Mappers;
public class EntityToExecutionPointer : IRegister
{
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.All };
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<WfPointerEntity, ExecutionPointer>()
            .Map(dest => dest.PersistenceData, src => JsonConvert.SerializeObject(src.PersistenceData ?? string.Empty, SerializerSettings))
            .Map(dest => dest.ContextItem, src => JsonConvert.SerializeObject(src.ContextItem ?? string.Empty, SerializerSettings))
            .Map(dest => dest.Children, src => src.Children.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            .Map(dest => dest.EventData, src => JsonConvert.SerializeObject(src.EventData ?? string.Empty, SerializerSettings))
            .Map(dest => dest.Outcome, src => JsonConvert.SerializeObject(src.Outcome ?? string.Empty, SerializerSettings))
            .Map(dest => dest.Status, src => (int)src.Status)
            .Map(dest => dest.Scope, src => src.Scope.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
    }
}
