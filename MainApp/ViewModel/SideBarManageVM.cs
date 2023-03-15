using CommunityToolkit.Mvvm.ComponentModel;
using MainApp.Common;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModel
{
    public partial class SideBarManageVM:ObservableObject
    {
        public SideBarManageVM()
        {

        }

        public ObservableCollection<SideBarItemInfo> InstalledItems => App.GetService<PluginLoader>().SideBarItemInfos;


    }
}
