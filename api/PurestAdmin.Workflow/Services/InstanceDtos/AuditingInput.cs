// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.InstanceDtos;
/// <summary>
/// 审批输入DTO
/// </summary>
public class AuditingInput
{
    /// <summary>
    /// 审批步骤
    /// </summary>
    public int StepId { get; set; }
    /// <summary>
    /// 是否同意
    /// </summary>
    public bool IsAgree { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string AuditingOpinion { get; set; }
}
