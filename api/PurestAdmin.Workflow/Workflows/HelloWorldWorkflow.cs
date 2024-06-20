using PurestAdmin.Workflow.Steps;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows
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
