// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 接口表
/// </summary>
[SugarTable("PUREST_INTERFACE_GROUP")]
public partial class InterfaceGroupEntity : BaseEntity
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
}