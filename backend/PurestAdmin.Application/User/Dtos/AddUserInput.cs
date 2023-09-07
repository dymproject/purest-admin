// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.User.Dtos;

/// <summary>
/// 用户添加
/// </summary>
public class AddUserInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 账号
    /// </summary>
    [MaxLength(36, ErrorMessage = "账号最大长度为：36")]
    [Required(ErrorMessage = "账号不能为空")]
    public string Account { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [MaxLength(36, ErrorMessage = "密码最大长度为：36")]
    public string Password { get; set; }
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