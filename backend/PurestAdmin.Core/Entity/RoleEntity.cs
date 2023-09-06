// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 角色
/// </summary>
[SugarTable("PUREST_ROLE")]
public partial class RoleEntity : BaseEntity
{
    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 角色描述
    /// </summary>
    [SugarColumn(ColumnName = "DESCRIPTION")]
    public string Description { get; set; }
}