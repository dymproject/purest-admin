// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PurestAdmin.Api.Host.Options;

public class SwaggerTagOrderFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc == null)
            return;

        // 使用已存在的 tags 或者从操作中构建
        var tags = swaggerDoc.Tags?.ToList() ?? BuildTagsFromOperations(swaggerDoc);

        if (tags == null || tags.Count == 0)
            return;

        // 把包含 "auth" 的 tag 放到最前（不区分大小写）
        var authTags = tags.Where(t => t.Name.IndexOf("auth", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        var otherTags = tags.Except(authTags).ToList();

        if (!authTags.Any())
            return;

        var ordered = new List<OpenApiTag>();
        ordered.AddRange(authTags);
        ordered.AddRange(otherTags);

        swaggerDoc.Tags = ordered;
    }

    private static List<OpenApiTag> BuildTagsFromOperations(OpenApiDocument swaggerDoc)
    {
        var tagNames = new List<string>();
        foreach (var path in swaggerDoc.Paths.Values)
        {
            foreach (var op in path.Operations.Values)
            {
                if (op.Tags == null) continue;
                foreach (var t in op.Tags)
                {
                    if (!tagNames.Contains(t.Name))
                    {
                        tagNames.Add(t.Name);
                    }
                }
            }
        }

        return tagNames.Select(n => new OpenApiTag { Name = n }).ToList();
    }
}
