// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;

namespace PurestAdmin.Multiplex.Contracts.Enums;
/// <summary>
/// 通用审批人类型枚举
/// </summary>
public enum GeneralAuditingAuditorTypeEnum
{
    /// <summary>
    /// 组织机构
    /// </summary>
    [Description("组织机构")]
    Organization = 0,
    /// <summary>
    /// 用户
    /// </summary>
    [Description("用户")]
    User = 1,
    /// <summary>
    /// 角色
    /// </summary>
    [Description("角色")]
    Role = 2,
}
