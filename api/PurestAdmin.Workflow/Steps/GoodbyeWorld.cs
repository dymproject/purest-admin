using Microsoft.Extensions.Logging;

using System;
using System.Linq;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Steps
{
    public class GoodbyeWorld : StepBody, ITransientDependency
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
