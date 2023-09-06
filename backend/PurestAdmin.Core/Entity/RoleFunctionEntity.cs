// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 角色页面
/// </summary>
[SugarTable("PUREST_ROLE_FUNCTION")]
public partial class RoleFunctionEntity : BaseEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [SugarColumn(ColumnName = "ROLE_ID")]
    public long RoleId { get; set; }
    /// <summary>
    /// 功能ID
    /// </summary>
    [SugarColumn(ColumnName = "FUNCTION_ID")]
    public long FunctionId { get; set; }
}