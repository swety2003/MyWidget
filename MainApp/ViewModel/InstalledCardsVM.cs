using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using PluginSDK;
using System.Collections.ObjectModel;

namespace MainApp.ViewModel
{
    public partial class InstalledCardsVM : ObservableObject
    {

        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;



        [RelayCommand]
        void AddCardToDesktop(object? info)
        {
            App.GetService<WidgetViewVM>().CreateCard(info as CardInfo);


        }
    }
}
