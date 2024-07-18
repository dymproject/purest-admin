// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.BackgroundArgs;
public class GeneralAuditingArgs
{
    /// <summary>
    /// 步骤Id
    /// </summary>
    public long ExecutionPointerId { get; set; }

    /// <summary>
	/// 审批人
	/// </summary>
    public long Auditor { get; set; }

    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string AuditorName { get; set; }

    /// <summary>
    /// 是否同意
    /// </summary>
    public bool IsAgree { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    public string AuditingOpinion { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime AuditingTime { get; set; }
}
