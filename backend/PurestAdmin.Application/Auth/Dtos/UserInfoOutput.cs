// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
