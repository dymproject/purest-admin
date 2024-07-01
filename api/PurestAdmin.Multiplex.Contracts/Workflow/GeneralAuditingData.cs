// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.Enums;

namespace PurestAdmin.Multiplex.Contracts.Workflow;
public class GeneralAuditingData
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public GeneralAuditingStatusEnum AuditingStatus { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    public string AuditingOpinion { get; set; }
}
