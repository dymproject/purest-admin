// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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