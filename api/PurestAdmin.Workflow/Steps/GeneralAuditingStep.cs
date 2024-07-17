// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.DataTypes;

using Volo.Abp.Timing;

namespace PurestAdmin.Workflow.Steps;
public class GeneralAuditingStep(ISqlSugarClient db, IClock clock, IWorkflowHost workflowHost) : StepBodyAsync
{
    private readonly ISqlSugarClient _db = db;
    private readonly IClock _clock = clock;
    private readonly IWorkflowHost _workflowHost = workflowHost;
    /// <summary>
    /// 审批人
    /// </summary>
    public long Auditor { get; set; }
    /// <summary>
    /// 审批人名称
    /// </summary>
    public string AuditorName { get; set; }
    /// <summary>
    /// 审批人类型
    /// </summary>
    public int AuditorType { get; set; }
    /// <summary>
    /// 审批步骤类型
    /// </summary>
    public int AuditingStepType { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        if (!context.ExecutionPointer.EventPublished)
        {
            context.ExecutionPointer.ExtensionAttributes[GeneralAuditingConst.AUDITOR] = Auditor;
            context.ExecutionPointer.ExtensionAttributes[GeneralAuditingConst.AUDITORNAME] = AuditorName;
            context.ExecutionPointer.ExtensionAttributes[GeneralAuditingConst.AUDITORTYPE] = AuditorType;
            context.ExecutionPointer.ExtensionAttributes[GeneralAuditingConst.AUDITINGSTEPTYPE] = AuditingStepType;
            var users = (GeneralAuditingAuditorTypeEnum)AuditorType switch
            {
                GeneralAuditingAuditorTypeEnum.Organization => await _db.Queryable<UserEntity>().Where(x => x.OrganizationId == Auditor).ToListAsync(),
                GeneralAuditingAuditorTypeEnum.User => await _db.Queryable<UserEntity>().Where(x => x.Id == Auditor).ToListAsync(),
                GeneralAuditingAuditorTypeEnum.Role => await _db.Queryable<UserEntity>().Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(ur => ur.UserId == x.Id).Any()).ToListAsync(),
                _ => await _db.Queryable<UserEntity>().Where(x => x.OrganizationId == Auditor).ToListAsync(),
            };
            await _db.Insertable(users.Select(u => new WfWaitingPointerEntity() { PointerId = context.ExecutionPointer.Id, UserId = u.Id }).ToList()).ExecuteReturnSnowflakeIdListAsync();
            return ExecutionResult.WaitForEvent(GeneralAuditingConst.EVENT, Guid.NewGuid().ToString(), DateTime.MinValue);
        }
        if (context.ExecutionPointer.EventData is not bool)
        {
            throw BusinessValidateException.Message("数据格式不正确");
        }
        var isAgree = ((bool)context.ExecutionPointer.EventData);
        if (!isAgree)
        {
            if (context.Workflow.Data is GeneralAuditingDefinition workflowData)
            {
                workflowData[nameof(GeneralAuditingDataResultEnum)] = (int)GeneralAuditingDataResultEnum.Discontinue;
            }
            return ExecutionResult.Outcome(context.Workflow.Data);

        }
        return ExecutionResult.Next();
    }
}