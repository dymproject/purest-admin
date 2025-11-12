// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.BackgroundService.Workers;

using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace PurestAdmin.BackgroundService;

[DependsOn(typeof(AbpBackgroundJobsModule))]
public class AdminBackgroundModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobWorkerOptions>(options =>
        {
            options.ApplicationName = context.Services.GetApplicationName()!;
            options.DefaultTimeout = 864000; //10 days (as seconds)
        });
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.AddBackgroundWorkerAsync<ClearRequestLogWorker>();
    }
}
