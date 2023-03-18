using System.Windows;

namespace PluginSDK.Controls
{
    public class CardWindow : Window
    {
        public ICard GetCard()
        {
            return this.Content as ICard;
        }
    }
}
