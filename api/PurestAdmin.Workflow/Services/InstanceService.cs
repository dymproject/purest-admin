// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.BackgroundArgs;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Workflow.DataTypes;
using PurestAdmin.Workflow.Services.InstanceDtos;

using Volo.Abp.BackgroundJobs;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// 流程实例服务
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
    public async Task<PagedList<InstanceOutput>> GetSelfPagedListAsync(GetSelfPagedListInput input)
    {
        var pagedList = await _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers)
            .WhereIF(input.WorkflowStatus.HasValue, x => x.Status == input.WorkflowStatus)
            .Where(x => x.CreateBy == _currentUser.Id)
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<InstanceOutput>>();
    }

    /// <summary>
    /// 待办事项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<WaitingAuditingOutput>> GetAuditingPagedListAsync(GetWaitingPagedListInput input)
    {
        var pagedList = await _db.Queryable<WfWorkflowEntity>()
            .LeftJoin<WfExecutionPointerEntity>((a, b) => a.PersistenceId == b.WorkflowId)
            .LeftJoin<UserEntity>((a, b, u) => a.CreateBy == u.Id)
            .Where((a, b, u) => SqlFunc.Subqueryable<WfWaitingPointerEntity>().Where(w => w.UserId == _currentUser.Id && b.Id == w.PointerId).Any())
            .Select((a, b, u) => new WfWorkflowEntity
            {
                PersistenceId = a.PersistenceId.SelectAll(),
                ExecutionPointers = SqlFunc.Subqueryable<WfExecutionPointerEntity>().Where(x => x.WorkflowId == a.PersistenceId).ToList(),
                CreateByName = u.Name,
            })
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<WaitingAuditingOutput>>();
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
        data.Add(nameof(GeneralAuditingDataResultEnum), GeneralAuditingDataResultEnum.Proceed);
        await _workflowHost.StartWorkflow(definition.DefinitionId, data);
    }

    /// <summary>
    /// 终止流程
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task TerminateAsync(long id)
    {
        var instance = await _db.Queryable<WfWorkflowEntity>().FirstAsync(x => x.PersistenceId == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        if (await _workflowHost.TerminateWorkflow(instance.InstanceId))
        {
            instance.Remark = "主动撤回";
            await _db.Updateable(instance).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 审批流程
    /// </summary>
    /// <returns></returns>
    public async Task AuditingAsync(long id, AuditingInput input)
    {
        var workflow = await _db.Queryable<WfWorkflowEntity>()
            .Includes(x => x.ExecutionPointers, a => a.ExtensionAttributes)
            .Where(x => x.PersistenceId == id).FirstAsync() ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        var pointer = workflow.ExecutionPointers.First(p => p.Status == (int)PointerStatus.WaitingForEvent);
        if (pointer.StepId != input.StepId) throw BusinessValidateException.Message("流程节点已完成。请刷新数据！");

        await _db.Deleteable<WfWaitingPointerEntity>().Where(x => x.PointerId == pointer.Id && x.UserId == _currentUser.Id).ExecuteCommandAsync();

        if (!input.IsAgree)
        {
            workflow.Remark = input.AuditingOpinion;
            await _db.Updateable(workflow).ExecuteCommandAsync();
        }

        await _backgroundJobManager.EnqueueAsync(new GeneralAuditingArgs()
        {
            AuditingOpinion = input.AuditingOpinion,
            IsAgree = input.IsAgree,
            AuditorName = _currentUser.Self.Name,
            Auditor = _currentUser.Id,
            AuditingTime = Clock.Now,
            ExecutionPointerId = pointer.PersistenceId
        });
    }
    /// <summary>
    /// 获取实例详情
    /// </summary>
    /// <param name="id">实例Id</param>
    /// <returns></returns>
    public async Task<InstanceOutput> GetInstanceDetailAsync(long id)
    {
        var result = await _db.Queryable<WfWorkflowEntity>()
            .Includes(w => w.Definition)
            .Includes(w => w.ExecutionPointers, p => p.AuditingRecords)
            .Where(w => w.PersistenceId == id)
            .FirstAsync();
        return result.Adapt<InstanceOutput>();
    }
}
