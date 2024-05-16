// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.DictDataServices.Dtos;

/// <summary>
/// 字典数据详情
/// </summary>
public class DictDataOutput
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
    /// 字典分类ID
    /// </summary>
    public long CategoryId { get; set; }
    /// <summary>
    /// 字典名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
}