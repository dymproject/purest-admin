// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.SystemConfig.Dtos;

/// <summary>
/// 系统配置表添加
/// </summary>
public class AddSystemConfigInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(40, ErrorMessage = "编码最大长度为：40")]
    public string ConfigCode { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    [MaxLength(1000, ErrorMessage = "值最大长度为：1000")]
    public string ConfigValue { get; set; }
}