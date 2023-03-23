using CommunityToolkit.Mvvm.ComponentModel;
using MyWidgets.APP.Common;
using MyWidgets.APP.Model;
using MyWidgets.SDK;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyWidgets.APP.ViewModel
{
    public class SideBarInstanceInfo : INotifyPropertyChanged
    {
        public SideBarInstanceInfo(SideBarItemInfo Item, bool Enabled)
        {
            this.Item = Item;
            this.Enabled = Enabled;
        }

        public SideBarItemInfo Item { get; }

        //private bool enabled => App.GetService<SideBarManageService>().Query(this.Item);

        public bool Enabled
        {
            get { return App.GetService<SideBarManageService>().Query(this.Item); }
            set
            {
                if (value)
                {
                    App.GetService<SideBarManageService>().Create(Item);
                }
                else
                {
                    App.GetService<SideBarManageService>().Remove(Item);
                }


                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public partial class SideBarManageVM : ObservableObject
    {

        AppConfig config => App.GetService<AppConfigManager>().Config;

        public SideBarManageVM()
        {
            GenInstanceInfo();
        }

        private void GenInstanceInfo()
        {

            //foreach (var item in sbii)
            //{
            //    InstalledItems.Add(new SideBarInstanceInfo(item, false));
            //}

            var sbi = App.GetService<PluginLoader>().SideBarItemInfos;

            foreach (var info in sbi)
            {
                bool enabled = false;
                foreach (var wid in config.EnabledSideBarItems)
                {
                    if (info.MainView.FullName == wid)
                    {
                        enabled = true;
                        //App.GetService<SideBarManageService>().Create(info, true);

                    }
                }

                InstalledItems.Add(new SideBarInstanceInfo(info, enabled));
            }

        }

        

        [ObservableProperty]
        ObservableCollection<SideBarInstanceInfo> installedItems = new ObservableCollection<SideBarInstanceInfo>();



    }
}
