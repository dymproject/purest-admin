// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Newtonsoft.Json;

namespace PurestAdmin.Workflow.Services.InstanceDtos;
/// <summary>
/// 待审批数据
/// </summary>
public class WaitingAuditingOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long PersistenceId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 流程发起人
    /// </summary>
    public string CreateByName { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// 步骤Id
    /// </summary>
    public int StepId => ExecutionPointers.First(p => p.Status == (int)PointerStatus.WaitingForEvent).StepId;

    /// <summary>
    /// 审批步骤
    /// </summary>
    [JsonIgnore]
    public List<ExecutionPointerOutput> ExecutionPointers { get; set; }
}
