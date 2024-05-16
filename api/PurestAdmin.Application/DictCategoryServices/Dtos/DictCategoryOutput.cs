// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictCategoryServices.Dtos;

/// <summary>
/// 字典分类详情
/// </summary>
public class DictCategoryOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    public string Code { get; set; }
}