// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Mapster;
using PurestAdmin.Workflow.Middleware;

using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

using WorkflowCore.Services.DefinitionStorage;

namespace PurestAdmin.Workflow;

public class AdminWorkflowModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddWorkflow(options =>
        {
            options.UsePersistence(sp => sp.GetService<AdminPersistenceProvider>());
        });
        context.Services.AddWorkflowDSL();
        context.Services.AddTransient<IDateTimeProvider, AdminDateTimeProvider>();
        context.Services.AddWorkflowMiddleware<PreWorkflowMiddleware>();
        context.Services.AutoRegisterStepBodys();

        context.Services.AddMapsterIRegister(Assembly.GetExecutingAssembly());
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(AdminWorkflowModule).Assembly, opts =>
            {
                opts.RootPath = "v1";
            });
        });
    }
    public override async void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var workflowHost = context.ServiceProvider.GetRequiredService<IWorkflowHost>();
        var loader = context.ServiceProvider.GetRequiredService<IDefinitionLoader>();
        var db = context.ServiceProvider.GetRequiredService<ISqlSugarClient>();

        workflowHost.OnStepError += (WorkflowInstance workflow, WorkflowStep step, Exception exception) =>
        {

        };
        workflowHost.Start();

        var definitions = await db.Queryable<WfDefinitionEntity>().Where(x => x.IsLocked == true).ToListAsync();
        foreach (var definition in definitions)
        {
            loader.LoadDefinition(definition.WorkflowContent, Deserializers.Json);
        }
    }
}
