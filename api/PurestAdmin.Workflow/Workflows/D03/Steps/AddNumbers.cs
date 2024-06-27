// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D03.Steps
{
    public class AddNumbers : StepBodyAsync
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public int Output { get; set; }


        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Output = Input1 + Input2;
            return ExecutionResult.Next();
        }
    }
}
