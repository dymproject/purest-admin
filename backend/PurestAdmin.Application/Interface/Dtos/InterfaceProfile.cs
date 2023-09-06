// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Interface.Dtos;
/// <summary>
/// 接口详情
/// </summary>
public class InterfaceProfile
{
    /// <summary>
    /// id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 接口名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 接口地址
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 请求方法
    /// </summary>
    public string RequestMethod { get; set; }
    /// <summary>
    /// 分组Id
    /// </summary>
    public long GroupId { get; set; }
}
