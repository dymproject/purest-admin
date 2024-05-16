// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.InterfaceServices.Dtos;
/// <summary>
/// 接口组
/// </summary>
public class InterfaceGroupOutput
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
    public List<InterfaceOutput> Interfaces { get; set; }
}
