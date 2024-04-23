// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.ComponentModel;
using System.Reflection;

namespace PurestAdmin.Application.UserServices.Dtos;

/// <summary>
/// 用户详情
/// </summary>
public class UserOutput
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
    /// 组织机构名称
    /// </summary>
    public string OrganizationName { get; set; }
    /// <summary>
    /// 角色Id
    /// </summary>
    public long RoleId { get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }
    /// <summary>
    /// 用户状态
    /// </summary>
    public int Status { get; set; }
}