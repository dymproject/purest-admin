// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Collections;
using System.Reflection;

using Microsoft.Extensions.Caching.Memory;

namespace PurestAdmin.Core.Cache;
public class AdminMemoryCache(IMemoryCache memoryCache) : IAdminCache
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Clear(string keyPrefix)
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var coherentState = _memoryCache.GetType().GetField("_coherentState", flags).GetValue(_memoryCache);
        var entries = coherentState.GetType().GetField("_entries", flags).GetValue(coherentState);
        var cacheItems = entries as IDictionary;
        foreach (DictionaryEntry cacheItem in cacheItems)
        {
            if (cacheItem.Key != null && cacheItem.Key.ToString().StartsWith(keyPrefix))
            {
                _memoryCache.Remove(cacheItem.Key);
            }
        }
    }

    public T? Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public async Task<T?> Get<T>(string key, Func<Task<T>> func)
    {
        var result = _memoryCache.Get<T>(key);
        if (result == null)
        {
            result = await func.Invoke();
            _memoryCache.Set(key, result);
        }
        return result;
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
            _memoryCache.Set(key, value);
        }
        else
        {
            _memoryCache.Set(key, value, expiry.Value);
        }
    }
}
