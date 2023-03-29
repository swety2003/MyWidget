using Microsoft.Extensions.Logging;
using MyWidgets.APP.Common;
using MyWidgets.APP.Model;
using MyWidgets.APP.ViewModel;
using MyWidgets.SDK;
using MyWidgets.SDK.Core.Card;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// WidgetView.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetView : Page
    {
        AppConfig appConfig;

        public WidgetView()
        {
            InitializeComponent();
            DataContext = App.GetService<WidgetViewVM>();
            appConfig = App.GetService<AppConfigManager>().Config;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var items = appConfig.CardInstances.Where(x=>x.Value.enabledProperty).ToList();

            foreach (var item in items)
            {
                App.GetService<CardManageService>().Enable(item.Key);
            }


            //App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);



        }
    }
}
