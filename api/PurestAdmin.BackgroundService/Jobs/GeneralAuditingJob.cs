// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using PurestAdmin.Multiplex.Contracts.BackgroundArgs;
using PurestAdmin.Multiplex.Contracts.Consts;
using PurestAdmin.Multiplex.Contracts.Enums;
using PurestAdmin.SqlSugar.Entity;

using SqlSugar;

using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

using WorkflowCore.Interface;

namespace PurestAdmin.BackgroundService.Jobs;
/// <summary>
/// 通用审批Job
/// </summary>
public class GeneralAuditingJob(ISqlSugarClient db, IWorkflowHost workflowHost) : AsyncBackgroundJob<GeneralAuditingArgs>, ITransientDependency
{
    private readonly ISqlSugarClient _db = db;
    private readonly IWorkflowHost _workflowHost = workflowHost;

    public override async Task ExecuteAsync(GeneralAuditingArgs args)
    {
        var auditingRecord = args.Adapt<WfAuditingRecordEntity>();
        await _db.Insertable(auditingRecord).ExecuteReturnSnowflakeIdAsync();
        var executionPointer = await _db.Queryable<WfExecutionPointerEntity>().Includes(x => x.ExtensionAttributes).FirstAsync(x => x.PersistenceId == args.ExecutionPointerId);
        var stepType = executionPointer.ExtensionAttributes.First(x => x.AttributeKey == GeneralAuditingConst.AUDITINGSTEPTYPE).AttributeValue;
        if (stepType == ((int)GeneralAuditingStepTypeEnum.Parallel).ToString() || !args.IsAgree)
        {
            var i = await _db.Deleteable<WfWaitingPointerEntity>().Where(x => x.PointerId == executionPointer.Id).ExecuteCommandAsync();
            await _workflowHost.PublishEvent(executionPointer.EventName, executionPointer.EventKey, args.IsAgree);
            return;
        }
        if (stepType == ((int)GeneralAuditingStepTypeEnum.Serial).ToString())
        {
            await _db.Deleteable<WfWaitingPointerEntity>().Where(x => x.PointerId == executionPointer.Id && x.UserId == args.Auditor).ExecuteCommandAsync();
            var count = await _db.Queryable<WfWaitingPointerEntity>().Where(x => x.PointerId == executionPointer.Id && x.UserId == args.Auditor).CountAsync();
            if (count == 0)
                await _workflowHost.PublishEvent(executionPointer.EventName, executionPointer.EventKey, args.IsAgree);
        }
    }
}
