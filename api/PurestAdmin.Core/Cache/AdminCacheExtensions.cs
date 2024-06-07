// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Core.Cache;
public static class AdminCacheExtensions
{
    public static IServiceCollection AddPurestCache(this IServiceCollection services)
    {
        services.AddSingleton<IAdminCache, AdminMemoryCache>();
        return services;
    }
}
