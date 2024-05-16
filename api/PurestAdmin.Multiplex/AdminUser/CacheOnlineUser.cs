// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
