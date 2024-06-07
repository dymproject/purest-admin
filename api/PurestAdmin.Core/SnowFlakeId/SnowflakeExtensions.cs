// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Yitter.IdGenerator;

namespace PurestAdmin.Core.SnowFlakeId;
public static class SnowflakeExtensions
{
    /// <summary>
    /// 引入雪花Id
    /// </summary>
    public static IServiceCollection AddSnowflakeId(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        var workerId = configuration.GetValue<ushort>("SnowflakeIdOptions:WorkId");

        var options = new IdGeneratorOptions
        {
            WorkerId = workerId
        };

        YitIdHelper.SetIdGenerator(options);

        return services;
    }
}