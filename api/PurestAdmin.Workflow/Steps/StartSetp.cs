// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者


namespace PurestAdmin.Workflow.Steps;
public class StartSetp : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return ExecutionResult.Next();
    }
}

