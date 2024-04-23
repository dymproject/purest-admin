// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Multiplex.AdminUser;
public class CacheOnlineUser(IPurestCache cache) : ICacheOnlineUser, ISingletonDependency
{
    private readonly IPurestCache _cache = cache;

    private const string USER_KEY = "online_user";

    /// <summary>
    /// 获取在线用户
    /// </summary>
    /// <returns></returns>
    public List<OnlineUserModel> GetOnlineUsers()
    {
        return _cache.Get<List<OnlineUserModel>>(USER_KEY) ?? [];
    }

    /// <summary>
    /// 设置在线用户
    /// </summary>
    /// <param name="users"></param>
    public void SetOnlineUser(List<OnlineUserModel> users)
    {
        _cache.Set(USER_KEY, users);
    }
}
