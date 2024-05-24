// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 系统文件实体
/// </summary>
[SugarTable("PUREST_PROFILE_SYSTEM")]
public partial class ProfileSystemEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 文件ID
    /// </summary>
    [SugarColumn(ColumnName = "FILE_ID")]
    public long FileId { get; set; }
}