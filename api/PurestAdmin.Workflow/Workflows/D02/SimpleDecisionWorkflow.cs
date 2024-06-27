// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.Workflows.D02.Steps;

using WorkflowCore.Interface;

namespace PurestAdmin.Workflow.Workflows.D02
{
    public class SimpleDecisionWorkflow : IWorkflow
    {
        public string Id => "Simple Decision Workflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<RandomOutput>(randomOutput =>
                {
                    randomOutput
                        .When(0)
                            .Then<CustomMessage>(cm =>
                            {
                                cm.Name("Print custom message");
                                cm.Input(step => step.Message, data => "Looping back....");
                            })
                            .Then(randomOutput);  //loop back to randomOutput

                    randomOutput
                        .When(1)
                            .Then<GoodbyeWorld>();
                });
        }
    }
}
