using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyWidgets.SDK.Core.Card
{
    public record CardInfo(ImageSource Icon, string Name, string Description, Type MainView, CardType CardType = CardType.UserControl);


    public interface ICard : IViewBase, IPreviewable
    {
        //支持多开
        public Guid GUID { get; }

        public int HeightPix { get; }
        public int WidthPix { get; }

        // 不能使用 CardInfo ICard.Info => info; ，否则无法绑定成功
        public CardInfo Info { get; }

        public void OnDestroyed();

    }


    public interface IPreviewable
    {
        public UIElement GetUIElement();


    }
    
}
