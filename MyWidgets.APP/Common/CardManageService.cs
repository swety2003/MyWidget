using MyWidgets.APP;
using MyWidgets.APP.View;
using Microsoft.Extensions.Logging;
using MyWidgets.APP.Model;
using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MyWidgets.APP.Common
{
    internal class CardManageService : INotifyPropertyChanged
    {
        ILogger<CardManageService> logger;

        public CardManageService(ILogger<CardManageService> logger)
        {
            this.logger = logger;
        }

        private AppConfig Config => App.GetService<AppConfigManager>().Config;

        private Canvas canvas => App.GetService<WidgetView>().cv;


        private IList<ICard> activateCards { get; set; } = new List<ICard>();

        public IList<ICard> ActiveCards { get { return activateCards; } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Create(CardInfo ci)
        {

            var guid = Guid.NewGuid();

            var card = Activator.CreateInstance(ci.MainView, guid) as ICard;

            switch (ci.CardType)
            {
                case CardType.Window:
                    {
                        CardWindow cw = new CardWindow { Content = card, Height = card.HeightPix, Width = card.WidthPix };
                        cw.LocationChanged += Win_LocationChanged;
                        cw.Show();

                    }
                    break;
                case CardType.UserControl:
                    {

                        CardControl mt = new CardControl { Content = card, HeightPix = card.HeightPix, WidthPix = card.WidthPix };
                        mt.OnCardMoved += Mt_OnCardMoved;
                        canvas.Children.Add(mt as UIElement);
                    }
                    break;
                default:
                    break;
            }

            logger.LogDebug($"创建了GUID为 {guid} 的{ci.Name}卡片");

            activateCards.Add(card);



            card.OnEnabled();

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveCards)));

            var c = new Card(ci.MainView.FullName, ci.CardType, new Point(0, 0));

            Config.CardInstances.Add(guid, c);

        }

        public void Remove(ICard card)
        {


            switch (card.Info.CardType)
            {
                case CardType.Window:

                    var w = card.GetCardWindow();
                    if (w != null)
                    {
                        w.Close();
                    }

                    break;
                case CardType.UserControl:
                    //var cc = (card as UserControl).Parent as CardControl;

                    canvas.Children.Remove(card.GetCardControl());
                    break;
                default:
                    break;
            }

            card.OnDisabled();

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveCards)));


            activateCards.Remove(card);



            Config.CardInstances.Remove(card.GUID);
        }


        private void Win_LocationChanged(object? sender, EventArgs e)
        {
            try
            {
                var w = sender as CardWindow;
                Config.CardInstances[w.GetCard().GUID].Pos = new Point(w.Left, w.Top);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }
        }

        private void Mt_OnCardMoved(CardControl sender, Point pos)
        {
            Config.CardInstances[sender.GetCard().GUID].Pos = pos;
        }

        internal void Create(CardInfo ci, KeyValuePair<Guid, Card> item)
        {
            var guid = item.Key;

            var card = Activator.CreateInstance(ci.MainView, guid) as ICard;

            switch (ci.CardType)
            {
                case CardType.Window:
                    {
                        CardWindow cw = new CardWindow { Content = card, Height = card.HeightPix, Width = card.WidthPix };
                        cw.LocationChanged += Win_LocationChanged;
                        cw.Left = item.Value.Pos.X;
                        cw.Top = item.Value.Pos.Y;
                        cw.Show();
                    }
                    break;
                case CardType.UserControl:
                    {

                        CardControl mt = new CardControl { Content = card, HeightPix = card.HeightPix, WidthPix = card.WidthPix };

                        mt.OnCardMoved += Mt_OnCardMoved;
                        canvas.Children.Add(mt);

                        Canvas.SetLeft(mt, item.Value.Pos.X);
                        Canvas.SetTop(mt, item.Value.Pos.Y);
                    }
                    break;
                default:
                    break;
            }

            logger.LogDebug($"创建了GUID为 {guid} 的{ci.Name}卡片");

            activateCards.Add(card);


            card.OnEnabled();

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveCards)));


        }
    }
}
