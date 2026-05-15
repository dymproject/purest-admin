// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

using PurestAdmin.Multiplex.AI.AIPlugins;

namespace PurestAdmin.Multiplex.AI;

public static class AIExtensions
{
    public static IServiceCollection AddAIService(this IServiceCollection services, IConfiguration configuration)
    {
        var aiOptions = configuration.GetSection("AIOptions").Get<AIOptions>() ?? throw new Exception("没有找到AI相关配置");
        if (string.IsNullOrEmpty(aiOptions.Endpoint) || string.IsNullOrEmpty(aiOptions.ModelName) || string.IsNullOrEmpty(aiOptions.ApiKey))
        {
            // 未配置 AI 相关配置，不启用 AI 功能
            return services;
        }
        //services.AddAntiforgery();如果使用 Cookie 存储用户信息，必须开启 Antiforgery。并配合[ValidateAntiForgeryToken]使用。
        services.AddKernel()
            .AddOpenAIChatCompletion(modelId: aiOptions.ModelName, apiKey: aiOptions.ApiKey, endpoint: new Uri(aiOptions.Endpoint))
            .Plugins.AddFromType<SystemPlugin>();
        return services;
    }
}
