// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 接口表
/// </summary>
public partial class InterfaceGroupEntity
{
    /// <summary>
    /// 接口列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(InterfaceEntity.GroupId))]
    public List<InterfaceEntity> Interfaces { get; set; }
}