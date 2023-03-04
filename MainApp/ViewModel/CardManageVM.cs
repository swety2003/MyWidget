using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using PluginSDK;
using System.Collections.ObjectModel;

namespace MainApp.ViewModel
{
    public partial class CardManageVM : ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;

        [ObservableProperty]
        CardInfo selectedCI;


        [RelayCommand]
        void AddCardToDesktop()
        {
            App.GetService<WidgetViewVM>().CreateCard(selectedCI);
        }
    }
}
