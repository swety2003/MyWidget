using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWidgets.APP.Common;
using MyWidgets.APP.View;
using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MyWidgets.APP.ViewModel
{
    public partial class CardManageVM : ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;


        [ObservableProperty]
        ObservableCollection<ICard>? cardInstances;


        [ObservableProperty]
        Visibility popOpen = Visibility.Visible;

        [RelayCommand]
        void GetCardDetail()
        {
            CardInstances = new ObservableCollection<ICard>();

            var activeCards = App.GetService<CardManageService>().ActiveCards;
            foreach (var card in activeCards)
            {
                if (card != null)
                {

                    CardInstances.Add(card);
                }
                else
                {
                    // Todo logger 
                }
            }
            PopOpen = Visibility.Visible;
        }

        [RelayCommand]
        void OpenInstalledCardPanel()
        {
            App.GetService<NavigationService>().NavigateToSub(App.GetService<InstalledCards>());
        }

        [RelayCommand]
        void CloseCard(ICard card)
        {
            App.GetService<CardManageService>().Remove(card);

            GetCardDetail();
        }

        [RelayCommand]
        void SetCardLock(ICard? card)
        {
            var uc = card as UserControl; if (uc != null)
            {
                var cc = uc.Parent as CardControl;

                cc?.SetLocked();
            }
        }

        [RelayCommand]
        void ShowCardSetting(object? thumb)
        {
            // todo ShowCardSetting
            //App.GetService<WidgetViewVM>().ShowCardSettingCommand(thumb);

        }
    }
}
