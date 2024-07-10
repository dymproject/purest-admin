// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.BackgroundArgs;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Workflow.DataTypes;
using PurestAdmin.Workflow.Services.WorkflowDtos;

using Volo.Abp.BackgroundJobs;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// 工作流服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.WORKFLOW)]
public class InstanceService(IWorkflowHost workflowHost, ISqlSugarClient db, ICurrentUser currentUser, IBackgroundJobManager backgroundJobManager) : ApplicationService
{
    private readonly IWorkflowHost _workflowHost = workflowHost;
    private readonly ISqlSugarClient _db = db;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

    /// <summary>
    /// 我的流程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<SelfPagedListOutput>> GetSelfPagedListAsync(GetSelfPagedListInput input)
    {
        var pagedList = await _db.Queryable<WfWorkflowEntity>()
            .WhereIF(input.WorkflowStatus.HasValue, x => x.Status == input.WorkflowStatus)
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<SelfPagedListOutput>>();
    }

    /// <summary>
    /// 待办事项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<WaitingAuditingOutput>> GetAuditingPagedListAsync(GetWaitingPagedListInput input)
    {
        var all = new List<SelfPagedListOutput>();
        var organizationTypeItemQuery = _db.Queryable<WfAuditingEntity>()
            .Where(x => x.Auditor == _currentUser.OrganizationId && x.Status == (int)input.Status);
        var userTypeItemQuery = _db.Queryable<WfAuditingEntity>()
            .Where(x => x.Auditor == _currentUser.Id && x.Status == (int)input.Status);
        var roleTypeItemQuery = _db.Queryable<WfAuditingEntity>()
            .Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(o => o.RoleId == x.Auditor && o.UserId == _currentUser.Id).Any() && x.Status == (int)input.Status);
        var query = _db.Union(organizationTypeItemQuery, userTypeItemQuery, roleTypeItemQuery);
        var pagedList = await query.LeftJoin<WfWorkflowEntity>((a, b) => a.InstanceId == b.InstanceId)
            .LeftJoin<WfDefinitionEntity>((a, b, c) => b.WorkflowDefinitionId == c.DefinitionId)
            .LeftJoin<UserEntity>((a, b, c, d) => b.CreateBy == d.Id)
            .Select((a, b, c, d) => new WaitingAuditingOutput()
            {
                Id = a.Id,
                CreateTime = a.CreateTime,
                WorkflowInstanceTitle = c.Name,
                CreateByName = d.Name,
                Version = c.Version
            }).ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList;
    }

    /// <summary>
    /// 开始流程
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task StartAsync(long id, GeneralAuditingDefinition data)
    {
        var definition = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        await _workflowHost.StartWorkflow(definition.DefinitionId, data);
    }
    /// <summary>
    /// 审核
    /// </summary>
    /// <returns></returns>
    public async Task AuditingAsync(long auditingId, AuditingInput input)
    {
        var auditingEntity = await _db.Queryable<WfAuditingEntity>().FirstAsync(x => x.Id == auditingId) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        var pointerEntity = await _db.Queryable<WfExecutionPointerEntity>().FirstAsync(x => x.Id == auditingEntity.ExecutionPointerId);
        if (!input.IsAgree)
        {
            //直接结束
            if (await _workflowHost.TerminateWorkflow(auditingEntity.InstanceId))
            {
                auditingEntity.Status = (int)GeneralAuditingStatusEnum.Unapproved;
                await _db.Updateable(auditingEntity).ExecuteCommandAsync();
                var workflowEntity = await _db.Queryable<WfWorkflowEntity>().FirstAsync(x => x.InstanceId == auditingEntity.InstanceId);
                workflowEntity.Remark = input.AuditingOpinion;
                await _db.Updateable(workflowEntity).ExecuteCommandAsync();
            }
            return;
        }
        if (auditingEntity.IsCountersign)
        {
            //插入会签人集合。等所有人同意之后，由后台作业推送下一步
            WfCountersignRecordEntity wfCountersignRecordEntity = new()
            {
                InstanceId = auditingEntity.InstanceId,
                ExecutionPointerId = auditingEntity.ExecutionPointerId,
                AuditingOpinion = input.AuditingOpinion,
                AuditingTime = Clock.Now,
                Auditor = _currentUser.Id
            };
            await _backgroundJobManager.EnqueueAsync(new CountersignAuditingArgs() { ExecutionPointerId = auditingEntity.ExecutionPointerId, InstanceId = auditingEntity.InstanceId });
            return;
        }
        await _workflowHost.PublishEvent(pointerEntity.EventName, pointerEntity.EventKey, input.AuditingOpinion);
    }
}
