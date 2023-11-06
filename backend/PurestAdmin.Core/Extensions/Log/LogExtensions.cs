// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PurestAdmin.Core.Extensions.Log;
public static class LogExtensions
{
    /// <summary>
    /// 引入内置日志组件
    /// </summary>
    public static IServiceCollection AddPurestAdminLogging(this IServiceCollection services)
    {
        Array.ForEach(new[] { LogLevel.Trace, LogLevel.Critical, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Debug }, logLevel =>
        {
            services.AddFileLogging($"{AppContext.BaseDirectory}\\logs\\{DateTime.Now:yyyy-MM-dd}\\{logLevel}.log", options =>
            {
                options.WriteFilter = logMsg => logMsg.LogLevel == logLevel;
            });
        });
        //[LoggingMonitor] 监听日志
        services.AddMonitorLogging();
        return services;
    }
}
