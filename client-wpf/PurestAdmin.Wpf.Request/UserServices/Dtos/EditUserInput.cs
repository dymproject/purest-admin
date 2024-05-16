// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Wpf.Request.UserServices.Dtos;

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