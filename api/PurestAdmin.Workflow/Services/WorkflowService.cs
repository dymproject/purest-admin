// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.AspNetCore.Mvc;

using PurestAdmin.Multiplex.Contracts.Consts;

using Volo.Abp.Application.Services;

using WorkflowCore.Interface;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// 工作流服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.WORKFLOW)]
public class WorkflowService(IWorkflowHost workflowHost) : ApplicationService
{
    private readonly IWorkflowHost _workflowHost = workflowHost;
    /// <summary>
    /// 开始流程
    /// </summary>
    /// <param name="definitionId"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task Start(string definitionId, object data)
    {
        await _workflowHost.StartWorkflow(definitionId, data);
    }
    /// <summary>
    /// 审核流程
    /// </summary>
    /// <returns></returns>
    //public async Task Auditing(string)
}
