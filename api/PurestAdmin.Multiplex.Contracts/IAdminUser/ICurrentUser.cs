// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.SqlSugar.Entity;


namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
/// <summary>
/// 当前用户
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 用户 Id
    /// </summary>
    long Id { get; }

    /// <summary>
    /// 角色Id
    /// </summary>
    long RoleId { get; }

    /// <summary>
    /// 组织机构Id
    /// </summary>
    long OrganizationId { get; }

    /// <summary>
    /// 用户实体
    /// </summary>
    public UserEntity Self { get; }

    /// <summary>
    /// 用户拥有的功能
    /// </summary>
    /// <returns></returns>
    Task<List<FunctionEntity>> GetFunctionsAsync();

    /// <summary>
    /// 用户拥有的接口
    /// </summary>
    /// <returns></returns>
    Task<List<InterfaceEntity>> GetInterfacesAsync();

    /// <summary>
    /// 用户的组织机构树
    /// </summary>
    /// <returns></returns>
    Task<List<OrganizationEntity>> GetOrganizationTreeAsync();
}
