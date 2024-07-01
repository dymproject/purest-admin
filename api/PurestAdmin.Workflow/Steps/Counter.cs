// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Steps;
public class Counter : StepBody
{
    public int Value { get; set; }
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Value++;
        return ExecutionResult.Next();
    }
}