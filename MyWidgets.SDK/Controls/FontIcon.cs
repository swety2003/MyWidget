using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyWidgets.SDK.Controls
{
    public class FontIcon:TextBlock
    {
        static FontIcon()
        {
            //FontFamilyProperty.OverrideMetadata(typeof(FontIcon), new FrameworkPropertyMetadata(Application.Current.FindResource("MaterialIconsRound")));
        }
    }
}
