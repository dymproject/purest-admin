// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D02.Steps
{
    public class CustomMessage : StepBody
    {

        public string Message { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Message);
            return ExecutionResult.Next();
        }
    }
}
