// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D18.Steps
{
    public class GoodbyeWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            return ExecutionResult.Next();
        }
    }
}
