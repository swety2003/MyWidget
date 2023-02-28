using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginSDK;
using System.Collections.ObjectModel;

namespace MainApp.ViewModel
{
    public partial class CardManageVM : ObservableObject
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
