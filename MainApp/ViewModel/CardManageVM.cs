using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using MainApp.View;
using PluginSDK;
using PluginSDK.Controls;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace MainApp.ViewModel
{
    public partial class CardManageVM : ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;


        [ObservableProperty]
        ObservableCollection<IPreviewable>? cardInstances;


        [ObservableProperty]
        Visibility popOpen = Visibility.Visible;

        [RelayCommand]
        void GetCardDetail()
        {
            CardInstances = new ObservableCollection<IPreviewable>();
            var activeCards = App.GetService<WidgetView>().cv.Children;

            var activeCardWindows = App.GetService<CardWindowManage>().AllCardWindows;
            foreach (var card in activeCards)
            {
                
                CardInstances.Add(card as IPreviewable);
            }
            foreach (var item in activeCardWindows)
            {
                CardInstances.Add(item);
            }
            PopOpen = Visibility.Visible;
        }

        [RelayCommand]
        void OpenInstalledCardPanel()
        {
            App.GetService<NavigationService>().NavigateToSub(App.GetService<InstalledCards>());
        }

        [RelayCommand]
        void CloseCard(object card)
        {
            if (card is CardControl)
            {

                CardControl t = (CardControl)card;

                var wvvm = App.GetService<WidgetViewVM>();
                wvvm.CloseCardCommand.Execute(t);


                GetCardDetail();
            }
            else if(card is Window)
            {
                var w = (Window)card;

                
            }
        }

        [RelayCommand]
        void SetCardLock(object? card)
        {
            if (card is CardControl)
            {

                CardControl t = (CardControl)card;
                t?.SetLocked();
            }
        }

        [RelayCommand]
        void ShowCardSetting(object? thumb)
        {
            //App.GetService<WidgetViewVM>().ShowCardSettingCommand(thumb);

        }
    }
}
