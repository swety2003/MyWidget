using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using MainApp.Model;
using PluginSDK;
using PluginSDK.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MainApp.ViewModel
{
    public partial class WidgetViewVM : ObservableObject
    {
        //[ObservableProperty]
        //private ObservableCollection<UIElement> activeCards = new ObservableCollection<UIElement>();

        public static Canvas cv;

        public static void CreateCard(CardInfo? ci)
        {
            if (ci == null)
            {
                return;
            }
            //var cards = App.GetService<WidgetViewVM>().ActiveCards;

            var wc = Activator.CreateInstance(ci.MainView, System.Guid.NewGuid()) as ICard ?? throw new Exception();


            MyThumb mt = new MyThumb { Content = wc, HeightPix = wc.HeightPix, WidthPix = wc.WidthPix };


            wc.OnEnabled();
            //cards.Add(mt);
            cv.Children.Add(mt);

            var card = new Card(ci.MainView.FullName, new Point());

            mt.OnCardMoved += Mt_OnCardMoved;

            App.appConfig.instances.Add(wc.GUID, card);
        }

        private static void Mt_OnCardMoved(MyThumb sender, System.Windows.Point pos)
        {

            App.appConfig.instances[sender.GetCard().GUID].Pos = pos;
        }

        [RelayCommand]
        void CloseCard(object o)
        {
            var Thumb = o as MyThumb;
            if (Thumb != null)
            {

                cv.Children?.Remove(Thumb);
                App.appConfig?.instances.Remove(Thumb.GetCard().GUID);
            }
        }
    }
}
