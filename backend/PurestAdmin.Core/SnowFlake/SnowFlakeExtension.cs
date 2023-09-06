// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Yitter.IdGenerator;

namespace PurestAdmin.Core.SnowFlake;
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