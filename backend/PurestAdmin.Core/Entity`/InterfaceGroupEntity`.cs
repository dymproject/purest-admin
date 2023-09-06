// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 接口表
/// </summary>
public partial class InterfaceGroupEntity : BaseEntity
{
    /// <summary>
    /// 接口列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(InterfaceEntity.GroupId))]
    public List<InterfaceEntity> Interfaces { get; set; }
}