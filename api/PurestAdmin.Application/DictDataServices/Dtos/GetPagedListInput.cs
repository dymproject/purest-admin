// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictDataServices.Dtos;

/// <summary>
/// 字典数据查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 字典分类Id
    /// </summary>
    [Required(ErrorMessage = "字典分类Id不能为空")]
    public long CategoryId { get; set; }
}