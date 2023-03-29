using MyWidgets.SDK.Core.Card;
using MyWidgets.SDK.Core.SideBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyWidgets.SDK.Extensions
{
    public static class UserControlExt
    {

        public static string GetPluginConfigFilePath(this IViewBase self)
        {
            string ret;

            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            if (self is ICard card)
            {
                ret = Path.Combine(abl, "Configs", $"{card.GUID}.json");
            }
            else if(self is ISideBarItem sbi)
            {

                ret = Path.Combine(abl, "Configs", $"config.json");
            }
            else
            {
                throw new NotSupportedException();
            }

            if (!Directory.Exists(Path.GetDirectoryName(ret)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ret));
            }
            return ret;
        }

        public static void ResizeCard(this UserControl self, int h, int w)
        {

            var t = (self as ICard)?.GetCardControl();

            if (t == null)
            {
                return;
            }
            t.HeightPix = h;
            t.WidthPix = w;
        }
    }
}
