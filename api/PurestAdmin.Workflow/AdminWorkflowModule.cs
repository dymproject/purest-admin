// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Workflow.Workflows;

using Volo.Abp;
using Volo.Abp.Modularity;

using WorkflowCore.Interface;

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
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var workflowHost = context.ServiceProvider.GetRequiredService<IWorkflowHost>();
        workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
        workflowHost.Start();
    }
}
