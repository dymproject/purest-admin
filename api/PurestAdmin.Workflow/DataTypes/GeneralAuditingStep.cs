// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者


namespace PurestAdmin.Workflow.DataTypes;
public class GeneralAuditingStep : StepBody
{
    public string EventKey { get; set; }

    public string EventName { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public object EventData { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (!context.ExecutionPointer.EventPublished)
        {
            DateTime effectiveDate = DateTime.MinValue;

            if (EffectiveDate != null)
            {
                effectiveDate = EffectiveDate.Value;
            }

            return ExecutionResult.WaitForEvent(EventName, EventKey, effectiveDate);
        }

        EventData = context.ExecutionPointer.EventData;
        return ExecutionResult.Next();
    }
}
