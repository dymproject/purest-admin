// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Core;

namespace FinalAdmin.Core.Entity;

/// <summary>
/// 系统配置表
/// </summary>
[SugarTable("PUREST_SYSTEM_CONFIG")]
public partial class SystemConfigEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "CONFIG_CODE")]
    public string ConfigCode { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    [SugarColumn(ColumnName = "CONFIG_VALUE")]
    public string ConfigValue { get; set; }
}