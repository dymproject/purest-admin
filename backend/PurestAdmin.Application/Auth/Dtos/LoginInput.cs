// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.Auth.Dtos;
/// <summary>
/// 登录模型
/// </summary>
[SuppressSniffer]
public class LoginInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空"), MinLength(3, ErrorMessage = "用户名不能少于 3 位字符")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空"), MinLength(5, ErrorMessage = "密码不能少于 5 位字符")]
    public string Password { get; set; }
}