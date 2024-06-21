// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;

using WorkflowCore.Models;

using Yitter.IdGenerator;

namespace PurestAdmin.Workflow.Mappers;
public class ExecutionPointerToEntity : IRegister
{
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.All };
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ExecutionPointer, WfPointerEntity>()
            .Map(dest => dest.PersistenceId, src => YitIdHelper.NextId())
            .Map(dest => dest.PersistenceData, src => JsonConvert.SerializeObject(src.PersistenceData, SerializerSettings))
            .Map(dest => dest.ContextItem, src => JsonConvert.SerializeObject(src.ContextItem, SerializerSettings))
            .Map(dest => dest.Children, src => string.Join(";", src.Children))
            .Map(dest => dest.EventData, src => JsonConvert.SerializeObject(src.EventData, SerializerSettings))
            .Map(dest => dest.Outcome, src => JsonConvert.SerializeObject(src.Outcome, SerializerSettings))
            .Map(dest => dest.Status, src => (int)src.Status)
            .Map(dest => dest.Scope, src => string.Join(";", src.Scope));
    }
}
