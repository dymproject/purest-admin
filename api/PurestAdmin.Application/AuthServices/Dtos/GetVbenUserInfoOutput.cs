// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
/// <summary>
/// 用户信息输出
/// </summary>
public class GetVbenUserInfoOutput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 用户名（account）
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }
    /// <summary>
    /// 角色
    /// </summary>
    public string[]? Roles { get; set; }
}
