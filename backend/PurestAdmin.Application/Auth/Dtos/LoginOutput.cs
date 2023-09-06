// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Auth.Dtos;
/// <summary>
/// 登录模型
/// </summary>
[SuppressSniffer]
public class LoginOutput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 在线Id
    /// </summary>
    public string OnlineId { get; set; }

    /// <summary>
    /// 权限集合
    /// </summary>
    public List<string> Permissions { get; set; } 

}
