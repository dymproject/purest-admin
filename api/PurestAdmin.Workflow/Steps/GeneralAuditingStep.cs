// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者



using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Workflow.DataTypes;

using Volo.Abp.Timing;

namespace PurestAdmin.Workflow.Steps;
public class GeneralAuditingStep(ISqlSugarClient db, ICurrentUser currentUser, IClock clock) : StepBodyAsync
{
    private readonly ISqlSugarClient _db = db;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IClock _clock = clock;

    /// <summary>
    /// 审核人
    /// </summary>
    public long Auditor { get; set; }

    /// <summary>
    /// 审核人类型
    /// </summary>
    public int AuditorType { get; set; }

    /// <summary>
    /// 是否会签
    /// </summary>
    public bool IsCountersign { get; set; }

    public async override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        if (!context.ExecutionPointer.EventPublished)
        {
            var aa = context.ExecutionPointer.EventData;
            var workflowInstance = await _db.Queryable<WfWorkflowEntity>().FirstAsync(x => x.InstanceId == context.Workflow.Id);
            var workflowDefinition = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.DefinitionId == workflowInstance.WorkflowDefinitionId);
            List<UserEntity> users = AuditorType switch
            {
                (int)GeneralAuditingAuditorTypeEnum.Organization => await _db.Queryable<UserEntity>().Where(x => x.OrganizationId == Auditor).ToListAsync(),
                (int)GeneralAuditingAuditorTypeEnum.User => await _db.Queryable<UserEntity>().Where(x => x.Id == Auditor).ToListAsync(),
                (int)GeneralAuditingAuditorTypeEnum.Role => await _db.Queryable<UserEntity>().Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(o => o.UserId == x.Id && o.RoleId == Auditor).Any()).ToListAsync(),
                _ => [],
            };
            //todo:待完善通知用户待办

            var auditing = new WfAuditingEntity()
            {
                WorkflowId = context.Workflow.Id,
                Auditor = Auditor,
                ExecutionPointerId = context.ExecutionPointer.Id,
                AuditorType = (int)AuditorType,
                IsCountersign = IsCountersign,
                Status = (int)GeneralAuditingStatusEnum.Waiting
            };
            await _db.Insertable(auditing).ExecuteReturnSnowflakeIdAsync();
            return ExecutionResult.WaitForEvent(GeneralAuditingConst.EVENT, Guid.NewGuid().ToString(), DateTime.MinValue);
        }
        GeneralAuditingData auditingData;
        if (context.ExecutionPointer.EventData is GeneralAuditingData data)
        {
            auditingData = data;
        }
        else
        {
            auditingData = new GeneralAuditingData()
            {
                AuditingOpinion = "同意",
                AuditingStatus = GeneralAuditingStatusEnum.Approved,
            };
        }
        var autitingEntity = await _db.Queryable<WfAuditingEntity>().FirstAsync(x => x.ExecutionPointerId == context.ExecutionPointer.Id);

        autitingEntity.AuditingOpinion = auditingData.AuditingOpinion;
        autitingEntity.AuditingTime = _clock.Now;
        autitingEntity.Status = (int)auditingData.AuditingStatus;
        await _db.Updateable(autitingEntity).ExecuteCommandAsync();
        return ExecutionResult.Next();
    }
}