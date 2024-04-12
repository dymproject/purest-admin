// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Core.Signalr;
public static class PurestSignalrExtensions
{
    public static IServiceCollection AddPurestSignalr(this IServiceCollection services)
    {
        services.AddSingleton<IUserIdProvider, PurestUserIdProvider>();
        services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
        {
            options.PayloadSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        });
        return services;
    }
}
