// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 系统文件实体扩展
///</summary>
public partial class ProfileSystemEntity
{
    /// <summary>
    /// 文件名 
    ///</summary>
    [SugarColumn(IsIgnore = true)]
    public string FileName { get; set; }
    /// <summary>
    /// 文件大小 
    ///</summary>
    [SugarColumn(IsIgnore = true)]
    public int FileSize { get; set; }
}
