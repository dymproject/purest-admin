// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 系统配置表
/// </summary>
[SugarTable("PUREST_SYSTEM_CONFIG")]
public partial class SystemConfigEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "CONFIG_CODE")]
    public string ConfigCode { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    [SugarColumn(ColumnName = "CONFIG_VALUE")]
    public string ConfigValue { get; set; }
}