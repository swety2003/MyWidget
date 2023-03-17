using CommunityToolkit.Mvvm.ComponentModel;
using MainApp.Common;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModel
{
    public class SideBarInstanceInfo:INotifyPropertyChanged
    {
        public SideBarInstanceInfo(SideBarItemInfo Item, bool Enabled)
        {
            this.Item = Item;
            this.Enabled = Enabled;
        }

        public SideBarItemInfo Item { get; }

        private bool enabled => App.GetService<SideBarManageService>().Query(this.Item);

        public bool Enabled
        {
            get { return enabled; }
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

    public partial class SideBarManageVM:ObservableObject
    {
        public SideBarManageVM()
        {
            GenInstanceInfo();
        }

        private void GenInstanceInfo()
        {
            var sbii = App.GetService<PluginLoader>().SideBarItemInfos;

            foreach (var item in sbii)
            {
                InstalledItems.Add(new SideBarInstanceInfo(item,false));
            }

        }

        [ObservableProperty]
        ObservableCollection<SideBarInstanceInfo> installedItems = new ObservableCollection<SideBarInstanceInfo>();



    }
}
