// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictDataServices.Dtos;

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
    [MaxLength(20, ErrorMessage = "字典名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 字典编码
    /// </summary>
    [MaxLength(20, ErrorMessage = "字典编码最大长度为：20")]
    public string Code { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }
}