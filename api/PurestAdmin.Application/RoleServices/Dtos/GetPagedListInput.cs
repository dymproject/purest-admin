// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.RoleServices.Dtos;

/// <summary>
/// 角色查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 角色名
    /// </summary>
    public string Name { get; set; }
}