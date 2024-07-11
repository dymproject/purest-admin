// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.InstanceDtos;
/// <summary>
/// 待审核数据
/// </summary>
public class SelfPagedListOutput
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
    /// 流程描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 流程状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string CurrentNodeName => ExecutionPointers.OrderByDescending(x => x.StepId).FirstOrDefault()?.StepName ?? "";

    /// <summary>
    /// 当前节点状态
    /// </summary>
    public string CurrentNodeStatusString
    {
        get
        {
            var currentNode = ExecutionPointers.OrderByDescending(x => x.StepId).FirstOrDefault();
            if (currentNode == null) return "";
            return (PointerStatus)currentNode.Status switch
            {
                PointerStatus.Legacy => "Legacy",
                PointerStatus.Pending => "待处理",
                PointerStatus.Running => "运行中",
                PointerStatus.Complete => "已完成",
                PointerStatus.Sleeping => "睡眠",
                PointerStatus.WaitingForEvent => "待审批",
                PointerStatus.Failed => "失败",
                PointerStatus.Compensated => "补偿",
                PointerStatus.Cancelled => "取消",
                PointerStatus.PendingPredecessor => "挂起前置任务",
                _ => ""
            };
        }
    }

    /// <summary>
    /// 审批节点
    /// </summary>
    public List<ExecutionPointerOutput> ExecutionPointers { get; set; } = [];
}
