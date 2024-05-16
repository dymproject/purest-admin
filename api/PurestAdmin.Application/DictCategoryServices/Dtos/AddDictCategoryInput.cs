// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictCategoryServices.Dtos;

/// <summary>
/// 字典分类添加
/// </summary>
public class AddDictCategoryInput
{
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
    /// <summary>
    /// 分类名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "分类名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    [MaxLength(20, ErrorMessage = "分类编码最大长度为：20")]
    public string Code { get; set; }
}