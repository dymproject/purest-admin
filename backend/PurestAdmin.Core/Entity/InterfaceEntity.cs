// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

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