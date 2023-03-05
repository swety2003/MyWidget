using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Common;
using MainApp.View;
using PluginSDK;
using PluginSDK.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace MainApp.ViewModel
{
    public partial class CardManageVM : ObservableObject
    {
        public ObservableCollection<CardInfo> CardInfos => App.GetService<PluginLoader>().CardInfos;


        [ObservableProperty]
        ObservableCollection<MyThumb>? cardInstances;


        [ObservableProperty]
        Visibility popOpen = Visibility.Visible;

        [RelayCommand]
        void GetCardDetail()
        {
            CardInstances = new ObservableCollection<MyThumb>();
            var activeCards = App.GetService<WidgetView>().cv.Children;
            foreach (var card in activeCards)
            {
                var thumb = card as MyThumb;
                if (thumb != null)
                {

                    CardInstances.Add(thumb);

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
        void CloseCard(MyThumb card)
        {
            var wvvm = App.GetService<WidgetViewVM>();
            wvvm.CloseCardCommand.Execute(card);


            GetCardDetail();
        }

        [RelayCommand]
        void SetCardLock(MyThumb? card)
        {
            card?.SetLocked();
        }

        [RelayCommand]
        void ShowCardSetting(MyThumb? thumb)
        {
            //App.GetService<WidgetViewVM>().ShowCardSettingCommand(thumb);

        }
    }
}
