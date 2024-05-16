// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;

using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Core.Ip2region;
/// <summary>
/// ip离线地址库
/// </summary>
/// <remarks>https://github.com/lionsoul2014/ip2region</remarks>
public static class Ip2regionExtensions
{
    /// <summary>
    /// 添加对象映射
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblies">扫描的程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddIp2region(this IServiceCollection services)
    {
        var hostingEnvironment = services.GetHostingEnvironment();
        services.AddSingleton<ISearcher>(new Searcher(CachePolicy.Content, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ip2region", "ip2region.xdb")));
        return services;
    }
}
