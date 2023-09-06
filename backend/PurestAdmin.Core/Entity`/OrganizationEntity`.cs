// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 组织机构
/// </summary>
public partial class OrganizationEntity
{
    /// <summary>
    /// 子集
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<OrganizationEntity> Children { get; set; }
}