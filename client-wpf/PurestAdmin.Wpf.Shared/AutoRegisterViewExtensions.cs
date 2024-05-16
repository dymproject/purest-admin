// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using Prism.Ioc;

using PurestAdmin.Wpf.Shared.AutoRegisterAttributes;

namespace PurestAdmin.Wpf.Shared
{
    public static class AutoRegisterViewExtensions
    {
        public static void AutoRegisterViewForNavigation(this IContainerRegistry containerRegistry)
        {
            var allType = Assembly.GetCallingAssembly().GetExportedTypes().Where(t => t.IsDefined(typeof(NavigationViewAttribute))).ToList();
            foreach (var item in allType)
            {
                var navigationViewAttribute = item.GetCustomAttribute<NavigationViewAttribute>();
                if (string.IsNullOrEmpty(navigationViewAttribute.Name))
                {
                    containerRegistry.RegisterForNavigation(item.UnderlyingSystemType, item.Name);
                }
                else
                {
                    containerRegistry.RegisterForNavigation(item.UnderlyingSystemType, navigationViewAttribute.Name);
                }
            }
        }
    }
}
