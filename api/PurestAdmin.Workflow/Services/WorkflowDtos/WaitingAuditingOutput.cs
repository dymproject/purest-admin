// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WorkflowDtos;
/// <summary>
/// 待审核数据
/// </summary>
public class WaitingAuditingOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 流程发起人
    /// </summary>
    public string CreateByName { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string WorkflowInstanceTitle { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public int Version { get; set; }
}
