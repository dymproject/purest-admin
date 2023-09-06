// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Role.Dtos;

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