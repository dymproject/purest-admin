// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.Caching.Memory;

namespace PurestAdmin.Core;
public class PurestMemoryCache : IPurestCache
{
    private readonly IMemoryCache _memoryCache;
    public PurestMemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public void Clear()
    {
        throw new NotImplementedException();
    }

    public T? Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public bool KeyExists(string key)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        if (expiry == null)
        {
            _memoryCache.Set<T>(key, value);
        }
        else
        {
            _memoryCache.Set<T>(key, value, expiry.Value);
        }
    }
}
