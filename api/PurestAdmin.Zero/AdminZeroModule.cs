// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core;
using PurestAdmin.SqlSugar;
using PurestAdmin.Workflow;

using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace PurestAdmin.Zero;
[DependsOn(typeof(AdminSqlSugarModule), typeof(AdminCoreModule), typeof(AdminWorkflowModule))]
public class AdminZeroModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IClock, Clock>();
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {

    }
}
