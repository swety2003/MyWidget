using MainApp.Common;
using MainApp.Model;
using MainApp.ViewModel;
using Microsoft.Extensions.Logging;
using PluginSDK;
using PluginSDK.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
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

            App.GetService<WidgetViewVM>().cv = cv;
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


                    if (item.Value.Wid == wid)
                    {
                        var wc = Activator.CreateInstance(ci.MainView, item.Key) as ICard;
                        if (wc == null)
                        {
                            wc = Activator.CreateInstance(ci.MainView) as ICard ?? throw new Exception($"加载{ci.MainView.FullName}失败！");
                        }

                        MyThumb mt = new MyThumb { Content = wc, HeightPix = wc.HeightPix, WidthPix = wc.WidthPix };

                        Canvas.SetLeft(mt, item.Value.Pos.X);
                        Canvas.SetTop(mt, item.Value.Pos.Y);

                        cv.Children.Add(mt);

                        wc.OnEnabled();

                        mt.OnCardMoved += Mt_OnCardMoved;


                    }
                }
            }

            //foreach (var item in App.CardInfos)
            //{

            //    //var wc = Activator.CreateInstance(item.MainView, System.Guid.NewGuid()) as ICard ?? throw new Exception();


            //    //MyThumb mt = new MyThumb { Content = wc, HeightPix = wc.HeightPix, WidthPix = wc.WidthPix };

            //    //cv.Children.Add(mt);

            //    //wc.OnEnabled();


            //}

        }

        private void Mt_OnCardMoved(MyThumb sender, Point pos)
        {

            appConfig.instances[sender.GetCard().GUID].Pos = pos;
        }
    }
}
