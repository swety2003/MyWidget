using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModel
{
    public partial class CardManageVM:ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.CardInfos;

        [ObservableProperty]
        CardInfo selectedCI;


        [RelayCommand]
        void AddCardToDesktop()
        {
            WidgetViewVM.CreateCard(selectedCI);
        }
    }
}
