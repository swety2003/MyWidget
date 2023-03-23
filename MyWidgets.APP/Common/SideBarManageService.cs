using MyWidgets.APP;
using Microsoft.Extensions.Logging;
using MyWidgets.APP.Model;
using MyWidgets.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MyWidgets.APP.Common
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

        public void Create(SideBarItemInfo ci, bool first_load = false)
        {

            try
            {

                var card = Activator.CreateInstance(ci.MainView, ContainerPop) as ISideBarItem;

                logger.LogDebug($"创建了{ci.Name}侧栏图标");

                activateItems.Add(card);

                Container.Children.Add(card as UIElement);

                card.OnEnabled();

                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveItems)));

                //var c = new Card(ci.MainView.FullName, ci.CardType, new Point(0, 0));

                //Config.instances.Add(guid, c);
                if (!first_load)
                {

                    Config.EnabledSideBarItems.Add(ci.MainView.FullName);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }

        }



        public void Remove(ISideBarItem card)
        {
            card.OnDisabled();


            Container.Children.Remove(card as UIElement);

            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(ActiveItems)));


            activateItems.Remove(card);

            //Config.instances.Remove(card.GUID);

            Config.EnabledSideBarItems.Remove(card.Info.MainView.FullName);

        }

        internal void Remove(SideBarItemInfo info)
        {
            foreach (var item in activateItems)
            {
                if (item.Info == info)
                {
                    Remove(item);
                }
            }
        }

        internal bool Query(SideBarItemInfo info)
        {
            foreach (var item in activateItems)
            {
                if (item.Info == info)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
