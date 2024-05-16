// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Wpf.Shared.CommonViewModels;

namespace PurestAdmin.Wpf.Platform.Models
{
    public class LoginModel : PuaValidateModel
    {
        private string _account;

        [MinLength(3, ErrorMessage = "账号不能小于3位"), Required(ErrorMessage = "请输入账号")]
        public virtual string Account
        {
            get { return _account; }
            set { _account = value; RaisePropertyChanged(); }
        }

        private string _password;
        [MinLength(6, ErrorMessage = "密码不能小于6位"), Required(ErrorMessage = "请输入密码")]
        public virtual string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }
    }
}
