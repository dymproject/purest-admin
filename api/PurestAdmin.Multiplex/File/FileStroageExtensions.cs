// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Multiplex.File;
public static class FileStroageExtensions
{
    public static IServiceCollection AddFileStorage(this IServiceCollection services)
    {
        services.AddScoped(typeof(IFileCommand<>), typeof(FileCommand<>));
        return services;
    }
}
