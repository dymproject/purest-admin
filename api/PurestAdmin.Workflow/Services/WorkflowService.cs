// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.DataTypes;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// 工作流服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.WORKFLOW)]
public class WorkflowService(IWorkflowHost workflowHost, ISqlSugarClient db, IPersistenceProvider persistenceProvider) : ApplicationService
{
    private readonly IWorkflowHost _workflowHost = workflowHost;
    private readonly ISqlSugarClient _db = db;
    private readonly IPersistenceProvider _persistenceProvider = persistenceProvider;
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
    public async Task AuditingAsync(long auditingId, GeneralAuditingData input)
    {
        var auditingEntity = await _db.Queryable<WfAuditingEntity>().FirstAsync(x => x.Id == auditingId) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        var pointerEntity = await _db.Queryable<WfExecutionPointerEntity>().FirstAsync(x => x.Id == auditingEntity.ExecutionPointerId);
        if (auditingEntity.IsCountersign && input.AuditingStatus == GeneralAuditingStatusEnum.Approved)
        {
            //插入会签人集合。等所有人同意之后，由后台服务推送下一步
            return;
        }
        await _workflowHost.PublishEvent(pointerEntity.EventName, pointerEntity.EventKey, input);
    }
    /// <summary>
    /// 终止流程
    /// </summary>
    /// <returns></returns>
    public async Task TerminateAsync(long instanceId, string Remark)
    {
        var workflowEntity = await _db.Queryable<WfWorkflowEntity>().FirstAsync(x => x.PersistenceId == instanceId) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        if (await _workflowHost.TerminateWorkflow(workflowEntity.InstanceId))
        {
            workflowEntity.Remark = Remark;
            await _db.Updateable(workflowEntity).ExecuteCommandAsync();
        }
    }
}
