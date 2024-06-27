// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D01.Steps
{
    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }
}
