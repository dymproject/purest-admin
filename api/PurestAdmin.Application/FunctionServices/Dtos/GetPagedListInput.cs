// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.FunctionServices.Dtos;

/// <summary>
/// 组织机构查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 功能名称
    /// </summary>
    public string Name { get; set; }
}