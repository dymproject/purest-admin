// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;
public partial class UserEntity
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public long RoleId { get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string RoleName { get; set; }
    /// <summary>
    /// 组织机构名称
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string OrganizationName { get; set; }
}
