// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.Workflows.D04;

using Volo.Abp.DependencyInjection;

using WorkflowCore.Interface;

namespace PurestAdmin.Zero;
public class WorkflowTestService(IWorkflowHost workflowHost) : ISingletonDependency
{
    private readonly IWorkflowHost _workflowhost = workflowHost;
    public void Initialization()
    {
        //_workflowhost.StartWorkflow("HelloWorld");
        var initialData = new MyDataClass();
        var workflowId = _workflowhost.StartWorkflow("EventSampleWorkflow", 1, initialData).Result;

        Console.WriteLine("Enter value to publish");
        string value = Console.ReadLine();
        _workflowhost.PublishEvent("MyEvent", workflowId, value);

    }
}
