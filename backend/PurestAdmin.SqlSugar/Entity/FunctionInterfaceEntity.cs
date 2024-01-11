// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 页面接口
/// </summary>
[SugarTable("PUREST_FUNCTION_INTERFACE")]
public partial class FunctionInterfaceEntity : BaseEntity
{
    /// <summary>
    /// 功能ID
    /// </summary>
    [SugarColumn(ColumnName = "FUNCTION_ID")]
    public long FunctionId { get; set; }
    /// <summary>
    /// 接口ID
    /// </summary>
    [SugarColumn(ColumnName = "INTERFACE_ID")]
    public long InterfaceId { get; set; }
}