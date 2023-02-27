using PluginSDK;
using PluginSDK.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Common
{
    public static class ThumbExt
    {
        public static ICard GetCard(this MyThumb self)
        {
            ICard? card = self.Content as ICard;


            return card ?? throw new ArgumentNullException(nameof(card));
        }

    }
}
