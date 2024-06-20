// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow.Steps;
public class GeneralAuditingStepBody : StepBodyAsync, ITransientDependency
{
    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Run(() => { });
        return ExecutionResult.Next();
    }
}
