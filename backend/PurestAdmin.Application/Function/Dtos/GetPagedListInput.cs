// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Function.Dtos;

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