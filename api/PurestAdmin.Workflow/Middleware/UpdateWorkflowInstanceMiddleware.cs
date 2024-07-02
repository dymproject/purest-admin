// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Serilog;

namespace PurestAdmin.Workflow.Middleware;
public class UpdateWorkflowInstanceMiddleware(ILogger logger) : IWorkflowStepMiddleware
{
    private readonly ILogger _logger = logger;

    public Task<ExecutionResult> HandleAsync(IStepExecutionContext context, IStepBody body, WorkflowStepDelegate next)
    {
        _logger.Information("当前工作流：{WorkflowDefinitionId}", context.Workflow.Id);
        _logger.Information("当前当前步骤Id：{WorkflowDefinitionId}", context.Step.Id);
        return next();
    }
}
