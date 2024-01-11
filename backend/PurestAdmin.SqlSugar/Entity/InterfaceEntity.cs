// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 接口表
/// </summary>
[SugarTable("PUREST_INTERFACE")]
public partial class InterfaceEntity : BaseEntity
{
    /// <summary>
    /// 接口名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 接口地址
    /// </summary>
    [SugarColumn(ColumnName = "PATH")]
    public string Path { get; set; }
    /// <summary>
    /// 请求方法
    /// </summary>
    [SugarColumn(ColumnName = "REQUEST_METHOD")]
    public string RequestMethod { get; set; }
    /// <summary>
    /// 分组Id
    /// </summary>
    [SugarColumn(ColumnName = "GROUP_ID")]
    public long GroupId { get; set; }
}