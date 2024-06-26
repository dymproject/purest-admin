using System;

using PurestAdmin.Workflow.Workflows.D11.Steps;

using WorkflowCore.Interface;

namespace PurestAdmin.Workflow.Workflows.D11
{
    public class IfWorkflow : IWorkflow<MyData>
    {
        public string Id => "if-sample";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
            builder
                .StartWith<SayHello>()
                .WaitFor("BranchEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
                .Output(data => data.Counter, step => step.EventData)
                .If(data => data.Counter < 3).Do(then => then
                    .StartWith<PrintMessage>()
                        .Input(step => step.Message, data => "Value is less than 3")
                )
                .If(data => data.Counter < 5).Do(then => then
                    .StartWith<PrintMessage>()
                        .Input(step => step.Message, data => "Value is less than 5")
                )
                .Then<SayGoodbye>();
        }
    }

    public class MyData
    {
        public int Counter { get; set; }
    }
}
