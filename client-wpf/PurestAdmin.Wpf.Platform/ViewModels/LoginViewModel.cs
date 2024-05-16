// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Prism.Services.Dialogs;

using PurestAdmin.Wpf.Platform.Models;
using PurestAdmin.Wpf.Request.AuthServices;
using PurestAdmin.Wpf.Request.AuthServices.Dots;


namespace PurestAdmin.Wpf.Platform.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly AuthService _authService;
        public LoginViewModel(AuthService authServie)
        {
            _authService = authServie;
        }

        public string Title => "登陆";

        public event Action<IDialogResult> RequestClose;

        public LoginModel LoginUser { get; set; } = new LoginModel { Account = "admin", Password = "123456" };

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(ExecuteLoginCommand);

        async void ExecuteLoginCommand()
        {
            if (LoginUser.Validate())
            {
                var res = await Rubyer.Dialog.Show(new Loading { Content = "正在登录，请稍等..." }, showCloseButton: false, openHandler: async dialog =>
                {
                    try
                    {
                        await _authService.LoginAsync(LoginUser.Adapt<LoginInput>());
                        RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        dialog.Close();
                    }
                });
            }
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
