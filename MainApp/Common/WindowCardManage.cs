using PluginSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Common
{
    internal class CardWindowManage
    {
        public List<IWindow> AllCardWindows { get; set; } = new List<IWindow>();

        public void Add(IWindow window)
        {
            AllCardWindows.Add(window);
        }

        public void Remove(IWindow window)
        {
            AllCardWindows.Remove(window);
        }
    }
}
