// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.Logging;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Workflows.D01.Steps
{
    public class GoodbyeWorld : StepBody
    {

        private ILogger _logger;

        public GoodbyeWorld(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GoodbyeWorld>();
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            _logger.LogInformation("Hi there!");
            return ExecutionResult.Next();
        }
    }
}
