using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWidgets.APP.Common;
using MyWidgets.APP.Model;
using MyWidgets.APP.View;
using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using MyWidgets.SDK.Core.Card;
using Panuon.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MyWidgets.APP.ViewModel
{
    public partial class CardManageVM : ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;


        [ObservableProperty]
        ObservableDictionary<Guid, Card> createdCards = new ObservableDictionary<Guid, Card>();

        //[ObservableProperty]
        //Visibility popOpen = Visibility.Visible;

        public CardManageVM()
        {

            var activeCards = App.GetService<AppConfigManager>().Config.CardInstances;

            foreach (var card in activeCards)
            {

                CreatedCards.Add(card.Key,card.Value);

            }
            //PopOpen = Visibility.Visible;
        }

        [RelayCommand]
        void OpenInstalledCardPanel()
        {
            App.GetService<NavigationService>().NavigateToSub(App.GetService<InstalledCards>());
        }

        [RelayCommand]
        void DestroyCard(Guid id)
        {
            App.GetService<CardManageService>().Remove(id);
            //GetCardDetail();
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
        void ShowCardSetting(Guid? guid)
        {
            // todo ShowCardSetting
            //App.GetService<WidgetViewVM>().ShowCardSettingCommand(thumb);
            if (guid==null)
            {
                return;
            }
            App.GetService<CardManageService>().ShowSetting((Guid)guid);
        }
    }
}
