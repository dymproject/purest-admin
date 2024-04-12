// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
    /// <summary>
    /// 完整路径 
    ///</summary>
    [SugarColumn(ColumnName = "FULL_PATH")]
    public string FullPath { get; set; }
}
