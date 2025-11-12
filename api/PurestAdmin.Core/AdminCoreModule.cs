// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Cache;
using PurestAdmin.Core.Ip2region;
using PurestAdmin.Core.Signalr;
using PurestAdmin.Core.SnowFlakeId;

using Serilog;
using Serilog.Events;

using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace PurestAdmin.Core
{
    [DependsOn(typeof(AbpAspNetCoreSignalRModule), typeof(AbpBlobStoringFileSystemModule))]
    public class AdminCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddSnowflakeId();
            context.Services.AddPurestCache();
            context.Services.AddPurestSignalr();
            context.Services.AddIp2region();

            ConfigSerilog(context);
            ConfigClock();
        }

        private void ConfigClock()
        {
            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });
        }



        private void ConfigSerilog(ServiceConfigurationContext context)
        {
            var template = "[{Timestamp:HH:mm:ss}] [{Level:u3}] {SourceContext} {NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            context.Services.AddSerilog(options =>
            {
                options.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                options.WriteTo.Console();
                options.WriteTo.File($"{AppContext.BaseDirectory}/logs/.txt", LogEventLevel.Warning, template, rollingInterval: RollingInterval.Day, shared: true);
            });
        }
    }
}
