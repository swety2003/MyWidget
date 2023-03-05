using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using MainApp.Model;
using Microsoft.Extensions.Logging;
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
        ILoggerFactory loggerFactory;

        public WidgetViewVM(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }
        public Canvas? cv;

        public AppConfig Config => App.GetService<AppConfigManager>().Config;

        public void CreateCard(CardInfo? ci)
        {
            if (ci == null)
            {
                return;
            }

            var wc = Activator.CreateInstance(ci.MainView, Guid.NewGuid()) as ICard ?? throw new Exception();


            MyThumb mt = new MyThumb { Content = wc, HeightPix = wc.HeightPix, WidthPix = wc.WidthPix };


            wc.OnEnabled();
            cv?.Children.Add(mt);

            var card = new Card(ci.MainView.FullName, new Point());

            mt.OnCardMoved += Mt_OnCardMoved;

            Config.instances.Add(wc.GUID, card);


            App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);
        }

        private void Mt_OnCardMoved(MyThumb sender, Point pos)
        {

            Config.instances[sender.GetCard().GUID].Pos = pos;
        }

        [RelayCommand]
        void CloseCard(object o)
        {
            var Thumb = o as MyThumb;
            if (Thumb != null)
            {
                Thumb.GetCard().OnDisabled();
                cv?.Children?.Remove(Thumb);
                Config?.instances.Remove(Thumb.GetCard().GUID);
            }

            App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);
        }

        [RelayCommand]
        void LockCard(object o)
        {
            var thumb = o as MyThumb;
            if (thumb != null)
            {
                var r = thumb.SetLocked();
                Config.instances[thumb.GetCard().GUID].Locked = r;
            }
        }


        [RelayCommand]
        void ShowCardSetting(MyThumb? thumb)
        {
            thumb?.GetCard().ShowSetting();
        }
    }
}
