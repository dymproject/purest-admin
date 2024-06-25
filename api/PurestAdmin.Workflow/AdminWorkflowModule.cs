// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Workflow.Workflows.D01;
using PurestAdmin.Workflow.Workflows.D02;
//using PurestAdmin.Workflow.Workflows.D03;
using PurestAdmin.Workflow.Workflows.D04;
using PurestAdmin.Workflow.Workflows.D18;

using Volo.Abp;
using Volo.Abp.Modularity;

using WorkflowCore.Interface;
using WorkflowCore.Models;

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

        context.Services.AutoRegisterStepBodys();
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var workflowHost = context.ServiceProvider.GetRequiredService<IWorkflowHost>();
        workflowHost.OnStepError += (WorkflowInstance workflow, WorkflowStep step, Exception exception) =>
        {

        };
        workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
        workflowHost.RegisterWorkflow<SimpleDecisionWorkflow>();
        //workflowHost.RegisterWorkflow<PassingDataWorkflow, MyDataClass>();
        //workflowHost.RegisterWorkflow<PassingDataWorkflow2, Dictionary<string, int>>();
        workflowHost.RegisterWorkflow<EventSampleWorkflow, MyDataClass>();
        workflowHost.RegisterWorkflow<ActivityWorkflow, MyData>();
        workflowHost.Start();
    }
}
