// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PurestAdmin.Api.Host.Options;

public class SortTagsAlphabeticallyFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Tags != null)
        {
            swaggerDoc.Tags = new HashSet<OpenApiTag>(swaggerDoc.Tags.OrderBy(t => t.Name));
        }
    }
}
