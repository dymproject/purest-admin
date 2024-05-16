// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Wpf.Shared.AutoRegisterAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NavigationViewAttribute : Attribute
    {
        public NavigationViewAttribute() { }

        /// <summary>
        /// 别名
        /// </summary>
        /// <param name="name"></param>
        public NavigationViewAttribute(string name) { }

        /// <summary>
        /// 别名
        /// </summary>
        public string Name { get; set; }
    }
}
