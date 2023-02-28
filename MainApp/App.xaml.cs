using MainApp.Common;
using MainApp.View;
using MainApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static Model.AppConfig appConfig;

        static IHost AppHost
        {
            get;
        }

        public static IEnumerable<IPlugin> Plugins = new ObservableCollection<IPlugin>();

        public static ObservableCollection<CardInfo> CardInfos = new ObservableCollection<CardInfo>();

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

                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug))
                .UseContentRoot(AppContext.BaseDirectory).ConfigureServices
                ((context, services) =>
                {
                    services.AddSingleton<WidgetView>();
                    services.AddTransient<CardManage>();
                    services.AddSingleton<Settings>();

                    #region ViewModel

                    services.AddSingleton<WidgetViewVM>();
                    services.AddSingleton<CardManageVM>();
                    #endregion

                }).Build();

            var s = Panuon.WPF.UI.Resources.StyleKeys.ContentControlXStyle;


            if (File.Exists(CONFIG_FILE))
            {
                appConfig = JsonConvert.DeserializeObject<Model.AppConfig>(File.ReadAllText(CONFIG_FILE)) ?? new Model.AppConfig();
            }
            else
            {
                appConfig = new Model.AppConfig();
            }
        }


        const string CONFIG_FILE = "config.json";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);




            try
            {
                Plugins = new PluginLoader().Load();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (var item in Plugins)
            {

                foreach (var c in item.GetAllCards())
                {
                    //var a = Activator.CreateInstance(c.mainView);
                    CardInfos.Add(c);
                }
            }
        }


        protected override void OnExit(ExitEventArgs e)
        {
            //Messages.SendOnExitMsg();

            base.OnExit(e);

            File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(appConfig));


        }
    }
}
