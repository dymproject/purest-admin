// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Prism.Mvvm;

using Rubyer;

namespace PurestAdmin.Wpf.Shared.CommonViewModels
{
    /// <summary>
    /// 汉堡包菜单模型
    /// </summary>
    public partial class PuaMenuItem : BindableBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public IconType? Icon { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string[] Permissions { get; set; }

        /// <summary>
        /// 页面名称
        /// </summary>
        public object View { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        public PuaMenuItem(string name, object view, string[] permissions, IconType? icon, bool isEnable = true)
        {
            Name = name;
            Icon = icon;
            View = view;
            Permissions = permissions;
            IsEnable = isEnable;
        }
    }
}
