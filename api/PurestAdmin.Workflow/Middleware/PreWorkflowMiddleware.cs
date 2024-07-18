// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.Timing;

namespace PurestAdmin.Workflow.Middleware;
public class PreWorkflowMiddleware(IClock clock) : IWorkflowMiddleware
{
    private readonly IClock _clock = clock;
    public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;

    public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
    {
        workflow.Description += _clock.Now.ToString("yyyyMMddHHmmss");
        return next();
    }
}