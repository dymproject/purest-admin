// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 文件上传记录表
///</summary>
[SugarTable("PUREST_FILE_RECORD")]
public class FileRecordEntity : BaseEntity
{
    /// <summary>
    /// 文件名 
    ///</summary>
    [SugarColumn(ColumnName = "FILE_NAME")]
    public string FileName { get; set; }
    /// <summary>
    /// 文件大小 
    ///</summary>
    [SugarColumn(ColumnName = "FILE_SIZE")]
    public int FileSize { get; set; }
    /// <summary>
    /// 文件扩展名 
    ///</summary>
    [SugarColumn(ColumnName = "FILE_EXT")]
    public string FileExt { get; set; }
}
