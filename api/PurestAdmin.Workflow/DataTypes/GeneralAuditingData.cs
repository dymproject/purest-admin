// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.DataTypes;
public class GeneralAuditingData
{
    /// <summary>
    /// 是否同意
    /// </summary>
    public int IsAgree { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    public string AuditingOpinion { get; set; }
}
