// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Yitter.IdGenerator;

namespace PurestAdmin.Core;
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