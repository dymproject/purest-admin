// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;

using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Mappers;
public class EventSubscriptionToEntity : IRegister
{
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.All };
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<EventSubscription, WfSubscriptionEntity>()
            .Map(dest => dest.SubscriptionData, src => JsonConvert.SerializeObject(src.SubscriptionData, SerializerSettings));
    }
}
