using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWidgets.APP.Common;
using MyWidgets.SDK;
using System.Collections.ObjectModel;

namespace MyWidgets.APP.ViewModel
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
