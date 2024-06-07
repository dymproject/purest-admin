// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Core.Cache;
public interface IAdminCache
{
    bool KeyExists(string key);
    void Set<T>(string key, T value, TimeSpan? expiry = null);
    T? Get<T>(string key);
    Task<T?> Get<T>(string key, Func<Task<T>> func);
    void Remove(string key);
    void Clear();
    void Clear(string keyPrefix);
}
