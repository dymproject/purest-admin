// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Core.Cache;
using PurestAdmin.Multiplex.Contracts.Consts;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Multiplex.AdminUser;
public class CacheOnlineUser(IAdminCache cache) : ICacheOnlineUser, ISingletonDependency
{
    private readonly IAdminCache _cache = cache;
    private static readonly object _lock = new();
    /// <summary>
    /// 获取在线用户
    /// </summary>
    /// <returns></returns>
    public List<OnlineUserModel> GetOnlineUsers()
    {
        return _cache.Get<List<OnlineUserModel>>(AdminClaimConst.ONLINE_USER) ?? [];
    }

    /// <summary>
    /// 设置在线用户
    /// </summary>
    /// <param name="users"></param>
    public void SetOnlineUser(List<OnlineUserModel> users)
    {
        lock (_lock)
        {
            _cache.Set(AdminClaimConst.ONLINE_USER, users);
        }
    }
    public bool TryUpdate(Func<List<OnlineUserModel>, List<OnlineUserModel>> updateFunc)
    {
        lock (_lock)
        {
            var current = GetOnlineUsers();
            var updated = updateFunc(current);
            if (updated is null)
                return false;

            SetOnlineUser(updated);
            return true;
        }
    }
}
