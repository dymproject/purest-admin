// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Role.Dtos;

/// <summary>
/// 角色详情
/// </summary>
public class RoleProfile
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
    /// 角色名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 角色描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// website权限
    /// </summary>
    public long[] WebsiteSecurityIds { get; set; }
    /// <summary>
    /// interface权限
    /// </summary>
    public long[] InterfaceSecurityIds { get; set; }
}