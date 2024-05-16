// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Threading.Tasks;
using System.Windows;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;

using PurestAdmin.Wpf.Entry.Views;
using PurestAdmin.Wpf.Platform;
using PurestAdmin.Wpf.Shared;

namespace PurestAdmin.Wpf.Entry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void OnInitialized()
        {
            #region 异常事件配置
            /* UI线程中的异常 UnhandledException 和  DispatcherUnhandledException 都会捕获 执行顺序是 DispatcherUnhandledException => UnhandledException
            当应用程序引发但未处理异常时出现，UI线程的异常,无法捕获多线程异常*/
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            //当某个异常未被捕获时出现,Thread多线程和UI线程都可以捕获
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //未被观察到的Task多线程异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            #endregion

            #region 配置默认导航页面
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("mainRegion", "Main");
            #endregion

            #region 显示登录界面
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("Login", (callback) =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Current.Shutdown();
                }
                base.OnInitialized();
            });
            #endregion
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<PlatformModule>();
            moduleCatalog.AddModule<SharedModule>();
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("---捕获Task异常---");
            e.SetObserved();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("---所有线程中的异常---");
            MessageBox.Show((e.ExceptionObject as Exception).Message);
            //在.Net6.0中无效 --- 在 app.config <runtime> 节点下添加 <legacyUnhandledExceptionPolicy enabled="1"/> 可以阻止应用程序奔溃类似 e.Handle=true
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("应用程序异常:" + e.Exception.Message);
            //Notification.Error(e.Exception.Message);
            // 表明该异常已被处理，不会造成程序崩溃和退出
            e.Handled = true;
        }
    }
}
