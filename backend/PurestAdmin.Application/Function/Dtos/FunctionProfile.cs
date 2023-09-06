// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Application.Function.Dtos;

/// <summary>
/// 组织机构详情
/// </summary>
public class FunctionProfile
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
    public string Code { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    public long? ParentId { get; set; }
    /// <summary>
    /// 子集
    /// </summary>
    public List<FunctionProfile> Children { get; set; }
}