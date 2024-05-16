// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Configuration;

using Prism.Mvvm;

namespace PurestAdmin.Wpf.Entry.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            Title = ConfigurationManager.AppSettings.Get("ApplicationName") ?? "PurestAdmin";
        }
    }
}
