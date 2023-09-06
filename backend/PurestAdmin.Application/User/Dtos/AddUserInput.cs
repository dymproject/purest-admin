// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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