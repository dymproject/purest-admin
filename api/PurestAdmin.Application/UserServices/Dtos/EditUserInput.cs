// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.UserServices.Dtos;

/// <summary>
/// 用户编辑
/// </summary>
public class EditUserInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    [MaxLength(20, ErrorMessage = "用户名最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    [MaxLength(11, ErrorMessage = "电话最大长度为：11")]
    public string Telephone { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    [MaxLength(20, ErrorMessage = "邮箱最大长度为：20")]
    public string Email { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public byte[] Avatar { get; set; }
    /// <summary>
    /// 组织机构Id
    /// </summary>
    [Required(ErrorMessage = "组织机构Id不能为空")]
    public long OrganizationId { get; set; }
    /// <summary>
    /// 角色Id
    /// </summary>
    [Required(ErrorMessage = "角色Id不能为空")]
    public long RoleId { get; set; }
}