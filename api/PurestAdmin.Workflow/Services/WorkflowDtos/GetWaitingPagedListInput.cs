// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WorkflowDtos;
public class GetWaitingPagedListInput : PaginationParams
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public GeneralAuditingStatusEnum Status { get; set; }
}
