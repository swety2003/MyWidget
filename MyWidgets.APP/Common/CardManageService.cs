using Microsoft.Extensions.Logging;
using MyWidgets.APP.Model;
using MyWidgets.APP.View;
using MyWidgets.APP.ViewModel;
using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using MyWidgets.SDK.Core.Card;
using MyWidgets.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyWidgets.APP.Common
{
    internal class CardManageService : INotifyPropertyChanged
    {
        ILogger<CardManageService> logger;

        public CardManageService(ILogger<CardManageService> logger)
        {
            this.logger = logger;
        }


        private Canvas canvas => App.GetService<WidgetView>().cv;


        private IList<ICard> activateCards { get; set; } = new List<ICard>();

        public IList<ICard> ActiveCards { get { return activateCards; } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Create(CardInfo ci)
        {

            var guid = Guid.NewGuid();


            logger.LogDebug($"创建了GUID为 {guid} 的{ci.Name}卡片");


            var c = new Card(ci.MainView.FullName, ci.CardType, new Point(0, 0),guid);


            App.GetService<CardManageVM>().CreatedCards.Add(guid, c);


            c.Enabled = true;
            //Enable(guid);

        }

        public void Enable(Guid guid)
        {
            var card_cfg = App.GetService<CardManageVM>().CreatedCards[guid];

            var cis = App.GetService<PluginLoader>().CardInfos;

            var ci = cis.Where(x => x.MainView.FullName == card_cfg.Wid).First();

            var card = Activator.CreateInstance(ci.MainView, guid) as ICard??throw new ArgumentNullException();

            switch (ci.CardType)
            {
                case CardType.Window:
                    {
                        CardWindow cw = new CardWindow { Content = card, Height = card.HeightPix, Width = card.WidthPix };
                        cw.LocationChanged += Win_LocationChanged;
                        cw.Left = card_cfg.Pos.X;
                        cw.Top = card_cfg.Pos.Y;
                        cw.Show();

                    }
                    break;
                case CardType.UserControl:
                    {

                        CardControl mt = new CardControl { Content = card, HeightPix = card.HeightPix, WidthPix = card.WidthPix };
                        mt.OnCardMoved += Mt_OnCardMoved;

                        Canvas.SetLeft(mt, card_cfg.Pos.X);
                        Canvas.SetTop(mt, card_cfg.Pos.Y);
                        canvas.Children.Add(mt);
                    }
                    break;
                default:
                    break;
            }



            logger.LogDebug($"启用了GUID为 {guid} 的{ci.Name}卡片");


            activateCards.Add(card);


            card.OnEnabled();


            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveCards)));


        }

        public void Disable(ICard c)
        {
            var guid = c.GUID;
            var cc = App.GetService<CardManageVM>().CreatedCards[guid]; if (cc != null)
            {

                c.OnDisabled();

                //cc.Enabled=false;
                if (c.Info.CardType==CardType.UserControl)
                {

                    canvas.Children.Remove(c.GetCardControl());
                }
                else
                {
                    c.GetCardWindow().Close();

                }


                activateCards.Remove(c);
                

            }
        }

        public void Disable(Guid guid)
        {
            try
            {

                var c = activateCards.Where(x => x.GUID == guid).First();

                Disable(c);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "禁用失败");
            }
        }

        public void Remove(Guid guid)
        {
            var c = activateCards.Where(x => x.GUID == guid).FirstOrDefault();
            if (c != null)
            {
                Disable(c);

                c.OnDestroyed();
            }
            App.GetService<CardManageVM>().CreatedCards.Remove(guid);
        }


        public void SetLocked(Guid guid,bool v)
        {
            var c = activateCards.Where(x => x.GUID == guid).FirstOrDefault();
            if (c==null)
            {
                return;
            }
            switch (c.Info.CardType)
            {
                case CardType.Window:
                    //c.GetCardWindow()
                    break;
                case CardType.UserControl:
                    c.GetCardControl().SetLocked(v);
                    break;
                default:
                    break;
            }
        }

        public void ShowSetting(Guid guid)
        {
            var c = activateCards.Where(x => x.GUID == guid).FirstOrDefault();
            if (c == null)
            {
                MessageBox.Show("请先启用卡片再进行设置！","不支持的操作",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            try
            {
                //可能未实现
                c.ShowSetting();
            }
            catch (Exception ex)
            {

            }
        }

        private void Win_LocationChanged(object? sender, EventArgs e)
        {
            try
            {
                var w = sender as CardWindow;
                App.GetService<CardManageVM>().CreatedCards[w.GetCard().GUID].Pos = new Point(w.Left, w.Top);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }
        }

        private void Mt_OnCardMoved(CardControl sender, Point pos)
        {
            App.GetService<CardManageVM>().CreatedCards[sender.GetCard().GUID].Pos = pos;
        }


    }
}
