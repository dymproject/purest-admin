// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Yitter.IdGenerator;

namespace PurestAdmin.Core.Extensions.SnowFlake;
public static class SnowflakeExtensions
{
    /// <summary>
    /// 引入雪花Id
    /// </summary>
    public static IServiceCollection AddSnowflakeId(this IServiceCollection services)
    {
        var workerId = App.Configuration.GetValue<ushort>("SnowflakeIdOption:WorkerId");

        var options = new IdGeneratorOptions();

        var isDevelopment = App.HostEnvironment.IsDevelopment();
        if (isDevelopment)
        {
            workerId = (ushort)(Math.Pow(2, options.WorkerIdBitLength) - 1);
        }
        options.WorkerId = workerId;

        YitIdHelper.SetIdGenerator(options);

        return services;
    }
}