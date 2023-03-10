using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
