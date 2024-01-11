// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.SqlSugar.Entity;


namespace PurestAdmin.Multiplex.MultiplexUser;
/// <summary>
/// 当前用户
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 获取用户 Id
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// 获取用户实体
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
