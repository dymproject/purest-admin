// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.DictData.Dtos;

/// <summary>
/// 字典数据添加
/// </summary>
public class AddDictDataInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 字典分类ID
    /// </summary>
    [Required(ErrorMessage = "字典分类ID不能为空")]
    public long CategoryId { get; set; }
    /// <summary>
    /// 字典名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "字典名称最大长度为：40")]
    public string Name { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }
}