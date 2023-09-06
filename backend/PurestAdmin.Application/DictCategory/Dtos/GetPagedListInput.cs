// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.DictCategory.Dtos;

/// <summary>
/// 字典分类查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}