// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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