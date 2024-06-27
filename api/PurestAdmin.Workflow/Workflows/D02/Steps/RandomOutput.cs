// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D02.Steps
{
    public class RandomOutput : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Random rnd = new Random();
            int value = rnd.Next(2);
            Console.WriteLine("Generated random value {0}", value);
            return ExecutionResult.Outcome(value);
        }
    }
}
