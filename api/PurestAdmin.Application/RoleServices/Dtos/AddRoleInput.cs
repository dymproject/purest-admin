// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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