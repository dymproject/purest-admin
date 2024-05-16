// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
/// <summary>
/// 用户信息输出
/// </summary>
public class GetUserInfoOutput
{
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 真实姓名
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
}
