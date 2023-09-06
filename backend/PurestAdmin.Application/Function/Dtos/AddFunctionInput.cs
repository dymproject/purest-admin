// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Function.Dtos;

/// <summary>
/// 组织机构添加
/// </summary>
public class AddFunctionInput
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
    public string Code { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    public long? ParentId { get; set; }
}