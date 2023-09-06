// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core;
/// <summary>
/// 业务日志错误码
/// </summary>
[ErrorCodeType]
public enum ErrorTipsEnum
{
    /// <summary>
    /// 记录不存在
    /// </summary>
    [Description("记录不存在"), ErrorCodeItemMetadata("记录不存在")]
    NoResult,
}
