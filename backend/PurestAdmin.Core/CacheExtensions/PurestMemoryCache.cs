// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.Caching.Memory;

namespace PurestAdmin.Core.CacheExtensions;
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
