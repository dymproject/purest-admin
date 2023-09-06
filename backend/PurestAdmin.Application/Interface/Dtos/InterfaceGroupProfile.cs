// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Interface.Dtos;
/// <summary>
/// 接口组
/// </summary>
public class InterfaceGroupProfile
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 接口详情
    /// </summary>
    public List<InterfaceProfile> Interfaces { get; set; }
}
