using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWidgets.SDK.Core.SideBar
{

    public interface ISideBarItem : IViewBase
    {
        public SideBarItemInfo Info { get; }
    }

    public record SideBarItemInfo(string Name, string Description, Type MainView);

    public delegate void ShowCard(UIElement element);
}
