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