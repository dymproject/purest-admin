// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Collections.ObjectModel;
using System.Windows;

using PurestAdmin.Wpf.Platform.Views;
using PurestAdmin.Wpf.Shared.CommonViewModels;

namespace PurestAdmin.Wpf.Platform.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<PuaMenuItem>
            {
                new PuaMenuItem("用户管理", new User(), new[] { "system.user" }, IconType.UserLine),
                new PuaMenuItem("角色管理", new Role(), new[] { "system.role" }, IconType.TeamLine),
                new PuaMenuItem("功能管理", new Function(), new[] { "system.function" }, IconType.FunctionLine),
                new PuaMenuItem("组织机构", new Organization(), new[] { "system.organization" }, IconType.OrganizationChart),
                new PuaMenuItem("字典管理", new Dictionary(), new[] { "system.dictionary" }, IconType.StickyNoteLine),
                new PuaMenuItem("配置管理", new SystemConfig(), new[] { "system.systemconfig" }, IconType.SettingsLine),
                new PuaMenuItem("通知公告", new Notice(), new[] { "system.notice" }, IconType.UserVoiceLine),
                new PuaMenuItem("在线用户", new OnlineUser(), new[] { "system.onlineuser" }, IconType.ContractLine),
                new PuaMenuItem("请求日志", new RequestLog(), new[] { "system.requestlog" }, IconType.EvernoteLine),
            };

            FilteredItems = new ObservableCollection<PuaMenuItem>(); // 先初始化为空
            FilterItems(); // 调用一次初始化
            CurrentViewItem = Items.First();
        }

        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<PuaMenuItem> Items { get; set; }

        private PuaMenuItem _currentViewItem;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public PuaMenuItem CurrentViewItem
        {
            get { return _currentViewItem; }
            set { SetProperty(ref _currentViewItem, value); }
        }

        private string _searchContent;
        /// <summary>
        /// 菜单搜索内容
        /// </summary>
        public string SearchContent
        {
            get { return _searchContent; }
            set
            {
                SetProperty(ref _searchContent, value);
                FilterItems(); // 调用过滤方法
            }
        }

        private ObservableCollection<PuaMenuItem> _filteredItems;
        /// <summary>
        /// 过滤后的子项集合
        /// </summary>
        public ObservableCollection<PuaMenuItem> FilteredItems
        {
            get { return _filteredItems; }
            set { SetProperty(ref _filteredItems, value); }
        }

        private void FilterItems()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchContent) ? Items : Items.Where(item => item.Name.Contains(SearchContent.Trim(), StringComparison.CurrentCultureIgnoreCase));

            FilteredItems.Clear();
            foreach (var item in filtered)
            {
                FilteredItems.Add(item);
            }
        }

        private bool _menuStatus = false;
        public bool MenuStatus
        {
            get { return _menuStatus; }
            set { SetProperty(ref _menuStatus, value); }
        }

        private DelegateCommand<object> _expandedCommand;
        public DelegateCommand<object> ExpandedCommand => _expandedCommand ??= new DelegateCommand<object>(ExecuteExpandedCommand);
        void ExecuteExpandedCommand(object parameter)
        {
            if (parameter is not RoutedEventArgs e)
            {
                MenuStatus = !MenuStatus;
            }
        }

        private DelegateCommand<object> _selectCommand;
        public DelegateCommand<object> SelectCommand => _selectCommand ??= new DelegateCommand<object>(ExecuteSelectCommand);
        void ExecuteSelectCommand(object view)
        {
            var type = view.GetType();
            var item = Items.FirstOrDefault(x => x.View.GetType() == type);
            if (item != null)
            {
                item.View = Activator.CreateInstance(type);
                CurrentViewItem = item;
            }
        }
    }
}
