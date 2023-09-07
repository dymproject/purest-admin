// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Core.Entity;

namespace PurestAdmin.Core.Multiplex
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// 获取用户 Id
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        UserEntity User { get; }

        /// <summary>
        /// 查询用户功能
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FunctionEntity>> GetFunctionsAsync(long userId);

        /// <summary>
        /// 查询用户接口
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<InterfaceEntity>> GetInterfacesAsync(long userId);
    }
}