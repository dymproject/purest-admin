// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Auth.Dtos;
/// <summary>
/// 用户信息输出
/// </summary>
public class UserInfoOutput
{
    /// <summary>
    /// 权限集合
    /// </summary>
    public List<string> Permissions { get; set; }
}
