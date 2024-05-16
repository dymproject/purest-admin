// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Flurl.Http.Configuration;

using Microsoft.Extensions.Caching.Memory;

using Prism.Ioc;
using Prism.Modularity;

using PurestAdmin.Wpf.Shared.HttpService;

namespace PurestAdmin.Wpf.Shared
{
    public class SharedModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
            containerRegistry.RegisterSingleton<IFlurlClientCache>(sp => new FlurlClientCache().Add("pua"));
            containerRegistry.Register<IPuaHttpService, PuaHttpService>();
        }
    }
}