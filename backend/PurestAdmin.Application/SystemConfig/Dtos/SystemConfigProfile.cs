// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.SystemConfig.Dtos;

/// <summary>
/// 系统配置表详情
/// </summary>
public class SystemConfigProfile
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string ConfigCode { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    public string ConfigValue { get; set; }
}