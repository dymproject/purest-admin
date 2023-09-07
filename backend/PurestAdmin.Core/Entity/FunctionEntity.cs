// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 功能实体
/// </summary>
[SugarTable("PUREST_FUNCTION")]
public partial class FunctionEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "CODE")]
    public string Code { get; set; }
    /// <summary>
    /// 隶属于
    /// </summary>
    [SugarColumn(ColumnName = "PARENT_ID")]
    public long? ParentId { get; set; }
}