// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.DictData.Dtos;

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