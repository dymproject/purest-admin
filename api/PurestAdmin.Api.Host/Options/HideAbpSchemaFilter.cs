// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PurestAdmin.Api.Host.Options
{
    public class HideAbpSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            context.SchemaRepository.Schemas.RemoveAll(item => item.Key.StartsWith("Volo."));
        }
    }
}
