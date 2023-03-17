using MainApp.Model;
using MainApp.View;
using Microsoft.Extensions.Logging;
using PluginSDK.Controls;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MainApp.Common
{
    internal class SideBarManageService
    {
        ILogger<SideBarManageService> logger;

        internal Popup ContainerPop;

        public SideBarManageService(ILogger<SideBarManageService> logger)
        {
            this.logger = logger;
        }

        private AppConfig Config => App.GetService<AppConfigManager>().Config;

        public StackPanel Container { get; set; }


        private IList<ISideBarItem> activateItems { get; set; } = new List<ISideBarItem>();

        public IList<ISideBarItem> ActiveItems { get { return activateItems; } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Create(SideBarItemInfo ci)
        {


            var card = Activator.CreateInstance(ci.MainView, ContainerPop) as ISideBarItem;


            logger.LogDebug($"创建了{ci.Name}侧栏图标");

            activateItems.Add(card);

            Container.Children.Add(card as UIElement);

            card.OnEnabled();

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveItems)));

            //var c = new Card(ci.MainView.FullName, ci.CardType, new Point(0, 0));

            //Config.instances.Add(guid, c);

        }

        public void Remove(ISideBarItem card)
        {
            card.OnDisabled();


            Container.Children.Remove(card as UIElement);

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveItems)));


            activateItems.Remove(card);

            //Config.instances.Remove(card.GUID);
        }

        internal void Remove(SideBarItemInfo info)
        {
            foreach (var item in activateItems)
            {
                if (item.Info==info)
                {
                    Remove(item);
                }
            }
        }

        internal bool Query(SideBarItemInfo info)
        {
            foreach (var item in activateItems)
            {
                return true;
            }
            return false;
        }
    }
}
