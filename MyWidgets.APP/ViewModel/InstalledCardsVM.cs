using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWidgets.APP.Common;
using MyWidgets.SDK;
using MyWidgets.SDK.Core.Card;
using System.Collections.ObjectModel;

namespace MyWidgets.APP.ViewModel
{
    public partial class InstalledCardsVM : ObservableObject
    {

        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;



        [RelayCommand]
        void AddCardToDesktop(CardInfo? info)
        {
            App.GetService<WidgetViewVM>().CreateCard(info);


        }
    }
}
