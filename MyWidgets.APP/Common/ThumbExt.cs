using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using System;

namespace MyWidgets.APP.Common
{
    public static class ThumbExt
    {
        public static ICard GetCard(this CardControl self)
        {
            ICard? card = self.Content as ICard;


            return card ?? throw new ArgumentNullException(nameof(card));
        }

    }
}
