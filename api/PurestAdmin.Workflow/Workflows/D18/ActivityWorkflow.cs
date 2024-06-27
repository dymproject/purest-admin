// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.Workflows.D18.Steps;

using WorkflowCore.Interface;

namespace PurestAdmin.Workflow.Workflows.D18
{
    class ActivityWorkflow : IWorkflow<MyData>
    {
        public string Id => "activity-sample";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Activity("get-approval", (data) => data.Request)
                    .Output(data => data.ApprovedBy, step => step.Result)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "Approved by " + data.ApprovedBy)
                .Then<GoodbyeWorld>();
        }
    }

    public class MyData
    {
        public string Request { get; set; }
        public string ApprovedBy { get; set; }
    }
}
