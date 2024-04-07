// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;

using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Core.Ip2Region;
/// <summary>
/// ip离线地址库
/// </summary>
/// <remarks>https://github.com/lionsoul2014/ip2region</remarks>
public static class Ip2RegionExtensions
{
    /// <summary>
    /// 添加对象映射
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblies">扫描的程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddIp2Region(this IServiceCollection services)
    {
        var hostingEnvironment = services.GetHostingEnvironment();
        services.AddSingleton<ISearcher>(new Searcher(CachePolicy.Content, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ip2Region", "ip2region.xdb")));
        return services;
    }
}
