using MyWidgets.SDK.Core.SideBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWidgets.SDK.Extensions
{

    public static class ISideBarItemExt
    {
        public static ShowCard? ShowCard { get; private set; }

        public static void SetMethod(ShowCard action)
        {
            if (ShowCard == null)
            {
                ShowCard = action;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static void Show(this ISideBarItem card, UIElement view)
        {
            ShowCard?.Invoke(view);
        }
    }
}
