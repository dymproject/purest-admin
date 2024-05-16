// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Core.Cache;
using PurestAdmin.Core.Ip2region;
using PurestAdmin.Core.Signalr;

using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PurestAdmin.Core
{
    [DependsOn(typeof(AbpAspNetCoreSignalRModule), typeof(AbpBackgroundJobsModule))]
    public class AdminCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSnowflakeId();
            context.Services.AddPurestCache();
            context.Services.AddPurestSignalr();
            context.Services.AddIp2region();
            Configure<AbpBackgroundJobWorkerOptions>(options =>
            {
                //默认是10天864000分钟
                options.DefaultTimeout = 30; //30分钟
            });
        }
    }
}
