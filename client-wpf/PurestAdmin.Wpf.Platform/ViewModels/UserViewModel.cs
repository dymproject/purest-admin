// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Wpf.Platform.Models.User;
using PurestAdmin.Wpf.Request.UserServices;
using PurestAdmin.Wpf.Request.UserServices.Dtos;
using PurestAdmin.Wpf.Shared.CommonViewModels;

namespace PurestAdmin.Wpf.Platform.ViewModels
{
    public class UserViewModel : BindableBase
    {
        #region 构造函数
        private readonly UserService _userService;
        public UserViewModel(UserService userService)
        {
            _userService = userService;
            SearchModel = new UserSearchModel();
        }
        #endregion

        #region 属性
        private UserSearchModel _searchModel;
        public UserSearchModel SearchModel
        {
            get { return _searchModel; }
            set { SetProperty(ref _searchModel, value); }
        }

        private PuaPagedResult<UserModel> _userPaginationModel;

        public PuaPagedResult<UserModel> UserPaginationModel
        {
            get { return _userPaginationModel; }
            set { SetProperty(ref _userPaginationModel, value); }
        }
        #endregion

        #region 命令

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand => _searchCommand ??= new DelegateCommand(ExecuteSearchCommand);

        async void ExecuteSearchCommand()
        {
            var result = await _userService.GetPagedListAsync(SearchModel.Adapt<GetPagedListInput>());
            UserPaginationModel = result.Adapt<PuaPagedResult<UserModel>>();
        }

        private DelegateCommand _resetCommand;
        public DelegateCommand ResetCommand => _resetCommand ??= new DelegateCommand(ExecuteResetCommand);

        void ExecuteResetCommand()
        {
            SearchModel = new();
        }

        private DelegateCommand<object> _pageSizeChangedCommand;
        public DelegateCommand<object> PageSizeChangedCommand =>
            _pageSizeChangedCommand ?? (_pageSizeChangedCommand = new DelegateCommand<object>(ExecutePageSizeChangedCommand));

        void ExecutePageSizeChangedCommand(object parameter)
        {
            SearchModel.PageSize = (int)parameter;
            ExecuteSearchCommand();
        }

        private DelegateCommand<object> _pageIndexChangedCommand;
        public DelegateCommand<object> PageIndexChangedCommand =>
            _pageIndexChangedCommand ?? (_pageIndexChangedCommand = new DelegateCommand<object>(ExecutePageIndexChangedCommand));

        void ExecutePageIndexChangedCommand(object parameter)
        {
            SearchModel.PageIndex = (int)parameter;
            ExecuteSearchCommand();
        }

        #endregion
    }
}
