// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 用户角色
/// </summary>
[SugarTable("PUREST_USER_ROLE")]
public partial class UserRoleEntity : BaseEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [SugarColumn(ColumnName = "ROLE_ID")]
    public long RoleId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public long UserId { get; set; }
}