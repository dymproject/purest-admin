// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Prism.Ioc;
using Prism.Modularity;

using PurestAdmin.Wpf.Platform.ViewModels;
using PurestAdmin.Wpf.Platform.Views;

namespace PurestAdmin.Wpf.Platform
{
    public class PlatformModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.AutoRegisterViewForNavigation();
            containerRegistry.RegisterDialog<Login, LoginViewModel>();
        }
    }
}