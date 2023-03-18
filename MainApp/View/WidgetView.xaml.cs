using MainApp.Common;
using MainApp.Model;
using MainApp.ViewModel;
using Microsoft.Extensions.Logging;
using PluginSDK;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// WidgetView.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetView : Page
    {
        AppConfig appConfig;
        ObservableCollection<CardInfo> cardInfos;
        ILoggerFactory loggerFactory;

        public WidgetView(ILoggerFactory loggerFactory)
        {
            InitializeComponent();
            DataContext = App.GetService<WidgetViewVM>();
            appConfig = App.GetService<AppConfigManager>().Config;
            cardInfos = App.GetService<PluginLoader>().CardInfos;
            this.loggerFactory = loggerFactory;

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            foreach (var item in appConfig.instances)
            {
                foreach (var ci in cardInfos)
                {
                    //插件标识
                    var wid = ci.MainView.FullName;


                    if (item.Value.Wid == wid && item.Value.CardType == ci.CardType)
                    {
                        if (ci.MainView.GetInterface("ICard") != null)
                        {
                            App.GetService<CardManageService>().Create(ci, item);

                            break;
                        }


                    }
                }
            }


            App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);



        }
    }
}
