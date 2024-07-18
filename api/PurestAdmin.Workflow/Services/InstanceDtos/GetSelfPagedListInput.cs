// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.InstanceDtos;
public class GetSelfPagedListInput : PaginationParams
{
    /// <summary>
    /// 工作流状态
    /// </summary>
    public int? WorkflowStatus { get; set; }
}
