// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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