// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.OrganizationServices.Dtos;

/// <summary>
/// 组织机构详情
/// </summary>
public class OrganizationOutput
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
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    public long? ParentId { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string Telephone { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    public string Leader { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 子集
    /// </summary>
    public List<OrganizationOutput> Children { get; set; }
}