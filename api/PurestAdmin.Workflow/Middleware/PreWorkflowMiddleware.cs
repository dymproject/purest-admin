// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Middleware;
public class PreWorkflowMiddleware : IWorkflowMiddleware
{
    public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;

    public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
    {
        return next();
    }
}