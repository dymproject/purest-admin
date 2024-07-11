// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.BackgroundArgs;
using PurestAdmin.Multiplex.Contracts.Enums;
using PurestAdmin.SqlSugar.Entity;
using PurestAdmin.Workflow.DataTypes;

using SqlSugar;

using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

using WorkflowCore.Interface;

namespace PurestAdmin.BackgroundService.Jobs;
/// <summary>
/// 会签审核
/// </summary>
public class CountersignAuditingJob(ISqlSugarClient db, IWorkflowHost workflowHost) : AsyncBackgroundJob<CountersignAuditingArgs>, ITransientDependency
{
    private readonly ISqlSugarClient _db = db;
    private readonly IWorkflowHost _workflowHost = workflowHost;

    public override async Task ExecuteAsync(CountersignAuditingArgs args)
    {
        var countersignRecordEntities = await _db.Queryable<WfCountersignRecordEntity>()
            .Where(x => x.ExecutionPointerId == args.ExecutionPointerId && x.InstanceId == args.InstanceId).ToListAsync();

        var auditingEntity = await _db.Queryable<WfAuditingEntity>()
            .Where(x => x.ExecutionPointerId == args.ExecutionPointerId && x.InstanceId == args.InstanceId).FirstAsync();

        List<UserEntity> users = auditingEntity.AuditorType switch
        {
            (int)GeneralAuditingAuditorTypeEnum.Organization => await _db.Queryable<UserEntity>().Where(x => x.OrganizationId == auditingEntity.Auditor).ToListAsync(),
            (int)GeneralAuditingAuditorTypeEnum.User => await _db.Queryable<UserEntity>().Where(x => x.Id == auditingEntity.Auditor).ToListAsync(),
            (int)GeneralAuditingAuditorTypeEnum.Role => await _db.Queryable<UserEntity>().Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(o => o.UserId == x.Id && o.RoleId == auditingEntity.Auditor).Any()).ToListAsync(),
            _ => [],
        };
        if (users.Select(x => x.Id).All(x => countersignRecordEntities.Select(x => x.Auditor).Contains(x)))
        {
            var pointerEntity = await _db.Queryable<WfExecutionPointerEntity>().FirstAsync(x => x.Id == auditingEntity.ExecutionPointerId);
            await _workflowHost.PublishEvent(pointerEntity.EventName, pointerEntity.EventKey, new GeneralAuditingData() { AuditingOpinion = "会签意见", IsAgree = 1 });
        }
    }
}
