// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar;
public abstract class BaseEntity
{
    /// <summary>
    /// Id
    ///</summary>
    [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
    public long Id { get; set; }

    /// <summary>
    /// 创建人
    ///</summary>
    [SugarColumn(ColumnName = "CREATE_BY")]
    public long CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    ///</summary>
    [SugarColumn(ColumnName = "CREATE_TIME")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 修改人
    ///</summary>
    [SugarColumn(ColumnName = "UPDATE_BY")]
    public long? UpdateBy { get; set; }

    /// <summary>
    /// 修改时间
    ///</summary>
    [SugarColumn(ColumnName = "UPDATE_TIME")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 备注 
    ///</summary>
    [SugarColumn(ColumnName = "REMARK")]
    public string Remark { get; set; }

}
