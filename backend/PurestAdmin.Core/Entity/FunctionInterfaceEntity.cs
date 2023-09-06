// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

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