// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Core;
public interface IPurestCache
{
    bool KeyExists(string key);
    void Set<T>(string key, T value, TimeSpan? expiry = null);
    T? Get<T>(string key);
    void Remove(string key);
    void Clear();
}
