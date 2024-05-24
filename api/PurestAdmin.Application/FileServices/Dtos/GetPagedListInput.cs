// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.FileServices.Dtos;

/// <summary>
/// 系统配置表查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}