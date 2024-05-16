// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Wpf.Platform.ViewModels
{
    public class WelcomeViewModel : BindableBase
    {
        private DelegateCommand test;
        public DelegateCommand CommandName =>
            test ?? (test = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            Message.Info("test");
        }
    }
}
