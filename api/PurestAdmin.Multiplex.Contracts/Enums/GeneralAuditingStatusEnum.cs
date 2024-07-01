// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;

namespace PurestAdmin.Multiplex.Contracts.Enums;
public enum GeneralAuditingStatusEnum
{
    /// <summary>
    /// 待审核
    /// </summary>
    [Description("待审核")]
    Waiting = 0,
    /// <summary>
    /// 审核通过
    /// </summary>
    [Description("审核通过")]
    Approved = 1,
    /// <summary>
    /// 审核未通过
    /// </summary>
    [Description("审核未通过")]
    Unapproved = 2,
}
