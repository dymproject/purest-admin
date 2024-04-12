// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.RoleServices.Dtos;

/// <summary>
/// 角色添加
/// </summary>
public class AddRoleInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "角色名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 角色描述
    /// </summary>
    [MaxLength(200, ErrorMessage = "角色描述最大长度为：200")]
    public string Description { get; set; }

    /// <summary>
    /// 权限Id集合
    /// </summary>
    public long[] SecurityIds { get; set; }
}