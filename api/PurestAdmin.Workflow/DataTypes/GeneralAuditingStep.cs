// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者



using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.Workflow;

using Volo.Abp.Timing;

namespace PurestAdmin.Workflow.DataTypes;
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
    public GeneralAuditingAuditorTypeEnum AuditorType { get; set; }

    /// <summary>
    /// 是否会签
    /// </summary>
    public bool IsCountersign { get; set; }

    /// <summary>
    /// 审核数据
    /// </summary>
    public GeneralAuditingData GeneralAuditingData { get; set; }

    public async override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        if (!context.ExecutionPointer.EventPublished)
        {
            var workflowInstance = await _db.Queryable<WfWorkflowEntity>().FirstAsync(x => x.InstanceId == context.Workflow.Id);
            var workflowDefinition = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.DefinitionId == workflowInstance.WorkflowDefinitionId);
            List<UserEntity> users = AuditorType switch
            {
                GeneralAuditingAuditorTypeEnum.Organization => await _db.Queryable<UserEntity>().Where(x => x.OrganizationId == Auditor).ToListAsync(),
                GeneralAuditingAuditorTypeEnum.User => await _db.Queryable<UserEntity>().Where(x => x.Id == Auditor).ToListAsync(),
                GeneralAuditingAuditorTypeEnum.Role => await _db.Queryable<UserEntity>().Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(o => o.UserId == x.Id && o.RoleId == Auditor).Any()).ToListAsync(),
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
        //var autitingEntity = await _db.Queryable<WfAuditingEntity>().FirstAsync(x => x.ExecutionPointerId == context.ExecutionPointer.Id);
        //if (GeneralAuditingData.AuditingStatus == GeneralAuditingStatusEnum.Unapproved)
        //{
        //    context.Workflow.Status = WorkflowStatus.Complete;
        //}
        //if (!IsCountersign)
        //{
        //    autitingEntity.AuditingOpinion = GeneralAuditingData.AuditingOpinion;
        //    autitingEntity.AuditingTime = _clock.Now;
        //    autitingEntity.Status = (int)GeneralAuditingData.AuditingStatus;
        //}
        //else
        //{

        //}
        return ExecutionResult.Next();
    }
}