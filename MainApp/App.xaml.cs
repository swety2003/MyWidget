using MainApp.Common;
using MainApp.View;
using MainApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        static IHost AppHost
        {
            get;
        }


        public static T GetService<T>() where T : class
        {

            if (AppHost.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} 你他妈没注册吧，找不到，去找上一个方法去。");
            }

            return service;
        }

        static App()
        {

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders()
                    .AddSimpleConsole(options =>
                    {
                        // 一条日志消息展示在同一行
                        options.SingleLine = true;
                        options.IncludeScopes = true;
                        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                        options.UseUtcTimestamp = false;
                    });
                    logging.SetMinimumLevel(LogLevel.Debug);

                })
                .UseContentRoot(AppContext.BaseDirectory).ConfigureServices
                ((context, services) =>
                {
                    services.AddSingleton<PluginLoader>();
                    services.AddSingleton<AppConfigManager>();

                    #region ViewModel

                    services.AddSingleton<WidgetViewVM>();
                    services.AddSingleton<CardManageVM>();
                    #endregion

                    services.AddSingleton<WidgetView>();
                    services.AddTransient<CardManage>();
                    services.AddSingleton<Settings>();





                }).Build();


        }



        protected override void OnStartup(StartupEventArgs e)
        {


            bool createNew;
            Mutex mutex = new Mutex(true, "swety.widget.app", out createNew);
            if (!createNew)
            {
                MessageBox.Show("Application is already run!");
                this.Shutdown();
            }


            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;



            App.GetService<AppConfigManager>().Load();

            App.GetService<PluginLoader>().Load();

            base.OnStartup(e);




        }


        protected override void OnExit(ExitEventArgs e)
        {
            //Messages.SendOnExitMsg();

            base.OnExit(e);

            App.GetService<AppConfigManager>().Save();

        }


        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
#if !DEBUG
            MessageBox.Show($"我们很抱歉，当前应用程序遇到一些问题，该操作已经终止:{e.Exception.Message}", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作

            File.WriteAllText("err.log", JsonConvert.SerializeObject(e.Exception));
            
            e.Handled = true;
#endif
        }
    }
}
