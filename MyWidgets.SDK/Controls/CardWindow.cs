using System.Windows;

namespace MyWidgets.SDK.Controls
{
    public class CardWindow : Window
    {
        public ICard GetCard()
        {
            return this.Content as ICard;
        }
    }
}
