// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

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