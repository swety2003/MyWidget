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
using System.Windows.Controls.Primitives;

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

            if (ci.MainView.IsAssignableFrom(typeof(ICard)))
            {

                var wc = Activator.CreateInstance(ci.MainView, Guid.NewGuid()) as ICard;


                CardControl mt = new CardControl { Content = wc, HeightPix = wc.HeightPix, WidthPix = wc.WidthPix };


                wc.OnEnabled();
                cv?.Children.Add(mt);

                var card = new Card(ci.MainView.FullName, new Point());

                mt.OnCardMoved += Mt_OnCardMoved;

                Config.instances.Add(wc.GUID, card);


                App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);
            }
            else
            {
                var wc = Activator.CreateInstance(ci.MainView, Guid.NewGuid()) as IWindow;
                var win = (wc as Window);
                win.Show();

                win.LocationChanged += Win_LocationChanged;

                App.GetService<CardWindowManage>().Add(wc);

                var card = new Card(ci.MainView.FullName, new Point());

                Config.instances.Add(wc.GUID, card);
            }

        }

        private void Win_LocationChanged(object? sender, EventArgs e)
        {
            var w = sender as Window;
            Config.instances[w.GetIWindow().GUID].Pos = new Point(w.Left,w.Top) ;
        }

        private void Mt_OnCardMoved(CardControl sender, Point pos)
        {
            Config.instances[sender.GetCard().GUID].Pos = pos;
        }

        [RelayCommand]
        void CloseCard(object o)
        {
            if (o is CardControl)
            {

                var Thumb = o as CardControl;
                if (Thumb != null)
                {
                    Thumb.GetCard().OnDisabled();
                    cv?.Children?.Remove(Thumb);
                    Config?.instances.Remove(Thumb.GetCard().GUID);
                }
            }
            else if (o is Window)
            {
                var w = o as Window;
                var iw = w.GetIWindow();

                iw.OnDisabled();
                w.Close();
                Config?.instances.Remove(w.GetIWindow().GUID);
                App.GetService<CardWindowManage>().Remove(iw);

            }

            App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);
        }

        [RelayCommand]
        void LockCard(object o)
        {
            var thumb = o as CardControl;
            if (thumb != null)
            {
                var r = thumb.SetLocked();
                Config.instances[thumb.GetCard().GUID].Locked = r;
            }
        }


        [RelayCommand]
        void ShowCardSetting(CardControl? thumb)
        {
            thumb?.GetCard().ShowSetting();
        }
    }
}
