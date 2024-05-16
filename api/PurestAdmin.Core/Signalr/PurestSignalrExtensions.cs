// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
