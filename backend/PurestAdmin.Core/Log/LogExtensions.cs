// 1

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PurestAdmin.Core.Log;
public static class LogExtensions
{
    /// <summary>
    /// 引入内置日志组件
    /// </summary>
    public static IServiceCollection AddFinalAdminLogging(this IServiceCollection services)
    {
        Array.ForEach(new[] { LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Debug }, logLevel =>
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
