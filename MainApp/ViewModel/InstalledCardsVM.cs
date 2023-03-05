using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class InstalledCardsVM:ObservableObject
    {

        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;


        [RelayCommand]
        void AddCardToDesktop(object? info)
        {
            App.GetService<WidgetViewVM>().CreateCard(info as CardInfo);


        }
    }
}
