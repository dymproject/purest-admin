// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Wpf.Shared.CommonViewModels;

namespace PurestAdmin.Wpf.Platform.Models.User
{
    public class UserSearchModel : PuaPagedParams
    {
        private string _account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); RaisePropertyChanged(); }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private int? _status;
        /// <summary>
        /// 用户状态
        /// </summary>
        public int? Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
    }
}
