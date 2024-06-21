// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;

using Yitter.IdGenerator;

namespace PurestAdmin.Workflow.Mappers;
public class ExtensionAttributeToEntity : IRegister
{
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.All };
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<KeyValuePair<string, object>, WfAttributeEntity>()
            .Map(dest => dest.PersistenceId, src => YitIdHelper.NextId())
            .Map(dest => dest.AttributeKey, src => src.Key)
            .Map(dest => dest.AttributeValue, src => JsonConvert.SerializeObject(src.Value, SerializerSettings));
    }
}
