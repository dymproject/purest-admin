// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
