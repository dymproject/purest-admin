// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core;
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
