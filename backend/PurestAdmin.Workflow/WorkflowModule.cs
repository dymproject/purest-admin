// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.AspNetCore.Mvc;

using WorkflowCore.Services.DefinitionStorage;

namespace PurestAdmin.Workflow;

public class WorkflowModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddWorkflowDSL();
        context.Services.AddWorkflow(x => x.UseMySQL(context.Services.GetConfiguration().GetConnectionString("workflow"), true, true));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetService<ILogger>();
        var host = context.ServiceProvider.GetService<IWorkflowHost>();
        var loader = context.ServiceProvider.GetService<IDefinitionLoader>();
        loader.LoadDefinition("{\"Id\":\"HelloWorld\",\"Version\":1,\"Steps\":[{\"Id\":\"Hello\",\"StepType\":\"MyApp.HelloWorld, MyApp\",\"NextStepId\":\"Bye\"},{\"Id\":\"Bye\",\"StepType\":\"MyApp.GoodbyeWorld, MyApp\"}]}", Deserializers.Json);
        if (host != null)
        {
            host.Start();
            host.StartWorkflow("HelloWorld");
            host.Stop();
        }
        logger?.Information("初始化工作流模块");
    }
}
