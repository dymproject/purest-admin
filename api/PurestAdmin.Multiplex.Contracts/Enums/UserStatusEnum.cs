// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;

namespace PurestAdmin.Multiplex.Contracts.Enums;
public enum UserStatusEnum
{
    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    Normal,
    /// <summary>
    /// 停用
    /// </summary>
    [Description("停用")]
    Stop
}
