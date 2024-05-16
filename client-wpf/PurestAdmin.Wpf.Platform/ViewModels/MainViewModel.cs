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
            Items =
            [
                new PuaMenuItem("用户管理",new User(),["system.user"],IconType.UserLine),
                new PuaMenuItem("角色管理",new Role(),["system.role"],IconType.TeamLine),
                new PuaMenuItem("功能管理",new Function(),["system.function"],IconType.FunctionLine),
                new PuaMenuItem("组织机构",new Organization(),["system.organization"],IconType.OrganizationChart),
                new PuaMenuItem("字典管理",new Dictionary(),["system.dictionary"],IconType.StickyNoteLine),
                new PuaMenuItem("配置管理",new SystemConfig(),["system.systemconfig"],IconType.SettingsLine),
                new PuaMenuItem("通知公告",new Notice(),["system.notice"],IconType.UserVoiceLine),
                new PuaMenuItem("在线用户",new OnlineUser(),["system.onlineuser"],IconType.ContractLine),
                new PuaMenuItem("请求日志",new RequestLog(),["system.requestlog"],IconType.EvernoteLine),
            ];
            CurrentViewItem = Items.First();
        }

        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<PuaMenuItem> Items { get; set; }

        private int _selectIndex;
        /// <summary>
        /// 选中项
        /// </summary>
        public int SelectIndex
        {
            get { return _selectIndex; }
            set { SetProperty(ref _selectIndex, value); }
        }

        private PuaMenuItem _currentViewItem;
        /// <summary>
        /// 当前选中
        /// </summary>
        public PuaMenuItem CurrentViewItem
        {
            get { return _currentViewItem; }
            set { _currentViewItem = value; }
        }

        private string _searchContent;
        /// <summary>
        /// 菜单搜索
        /// </summary>
        public string SearchContent
        {
            get { return _searchContent; }
            set { _searchContent = value; }
        }

        private DelegateCommand<object> _expandedCommand;
        public DelegateCommand<object> ExpandedCommand =>
            _expandedCommand ?? (_expandedCommand = new DelegateCommand<object>(ExecuteExpandedCommand));

        void ExecuteExpandedCommand(object parameter)
        {
            var e = parameter as RoutedEventArgs;
        }


        private DelegateCommand<object> _selectCommand;
        public DelegateCommand<object> SelectCommand => _selectCommand ??= new DelegateCommand<object>(ExecuteSelectCommand);

        void ExecuteSelectCommand(object view)
        {
            var type = view.GetType();
            var item = Items.First(x => x.View.GetType() == type);
            item.View = Activator.CreateInstance(type);
        }
    }
}
