// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictCategoryServices.Dtos;

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