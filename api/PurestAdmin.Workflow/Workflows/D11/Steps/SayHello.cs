// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D11.Steps
{
    public class SayHello : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello");
            return ExecutionResult.Next();
        }
    }
}
