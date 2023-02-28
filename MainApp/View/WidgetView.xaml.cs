using MainApp.Common;
using MainApp.ViewModel;
using PluginSDK;
using PluginSDK.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// WidgetView.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetView : Page
    {
        public WidgetView()
        {
            InitializeComponent();
            var vm = App.GetService<WidgetViewVM>();
            WidgetViewVM.cv = cv;
            DataContext = vm;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            foreach (var item in App.appConfig.instances)
            {
                foreach (var ci in App.CardInfos)
                {
                    //插件标识
                    var wid = ci.MainView.FullName;


                    if (item.Value.Wid == wid)
                    {
                        var wc = Activator.CreateInstance(ci.MainView, item.Key) as ICard ?? throw new Exception();


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

            App.appConfig.instances[sender.GetCard().GUID].Pos = pos;
        }
    }
}
