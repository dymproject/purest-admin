// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Cache;
using PurestAdmin.Core.File;
using PurestAdmin.Core.Ip2region;
using PurestAdmin.Core.Signalr;

using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace PurestAdmin.Core
{
    [DependsOn(typeof(AbpAspNetCoreSignalRModule), typeof(AbpBlobStoringFileSystemModule))]
    public class AdminCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddSnowflakeId();
            context.Services.AddPurestCache();
            context.Services.AddPurestSignalr();
            context.Services.AddIp2region();

            Configure<AbpBlobStoringOptions>(options =>
            {
                var fileStorageOptions = configuration.GetSection("FileStorageOptions");
                options.Containers.Configure<ProfileSystemContainer>(container =>
                {
                    var type = fileStorageOptions["ProfileSystemContainer:Type"];
                    if (type == Enum.GetName(typeof(StorageTypeEnum), StorageTypeEnum.File))
                    {
                        container.UseFileSystem(fileSystem =>
                        {
                            fileSystem.BasePath = fileStorageOptions["ProfileSystemContainer:Path"] ?? AppContext.BaseDirectory;
                        });
                    }
                });
            });
        }
    }
}
