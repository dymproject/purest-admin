// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.Workflows.D01.Steps;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D01
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }

        public string Id => "HelloWorld";

        public int Version => 1;

    }
}
