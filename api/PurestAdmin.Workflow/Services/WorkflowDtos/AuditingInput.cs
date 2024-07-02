// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WorkflowDtos;
/// <summary>
/// 审核输入DTO
/// </summary>
public class AuditingInput
{
    /// <summary>
    /// 是否同意
    /// </summary>
    public bool IsAgree { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    public string AuditingOpinion { get; set; }
}
