// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

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