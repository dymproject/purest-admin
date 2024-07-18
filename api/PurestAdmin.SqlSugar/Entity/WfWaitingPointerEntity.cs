// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 待审批数据表
/// </summary>
[SugarTable("PUREST_WF_WAITING_POINTER")]
public partial class WfWaitingPointerEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
    public long Id { get; set; }
    /// <summary>
    /// 用户Id
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public long UserId { get; set; }
    /// <summary>
    /// 步骤Id
    /// </summary>
    [SugarColumn(ColumnName = "POINTER_ID")]
    public string PointerId { get; set; }
}