// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;

using WorkflowCore.Models;

using Yitter.IdGenerator;

namespace PurestAdmin.Workflow.Mappers;
public class WorkflowInstanceToEntity : IRegister
{
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.All };
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<WorkflowInstance, WfWorkflowEntity>()
            .Map(dest => dest.PersistenceId, src => YitIdHelper.NextId())
            .Map(dest => dest.Data, src => JsonConvert.SerializeObject(src.Data, SerializerSettings));
    }
}
