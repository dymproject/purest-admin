// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Role.Dtos;

namespace PurestAdmin.Application.User.Dtos;

/// <summary>
/// 用户详情
/// </summary>
public class UserProfile
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string Account { get; set; }
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
    /// <summary>
    /// 头像
    /// </summary>
    public byte[] Avatar { get; set; }
    /// <summary>
    /// 组织机构Id
    /// </summary>
    public long OrganizationId { get; set; }
    /// <summary>
    /// 角色Id
    /// </summary>
    public long RoleId { get; set; }
    /// <summary>
    /// 角色
    /// </summary>
    public RoleProfile Role { get; set; }
}