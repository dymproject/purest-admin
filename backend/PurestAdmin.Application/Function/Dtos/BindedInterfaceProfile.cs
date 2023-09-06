// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Function.Dtos;
public class BindedInterfaceProfile
{
    /// <summary>
    /// Id
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
    /// 接口Id 
    /// </summary>
    public long InterfaceId { get; set; }
}
