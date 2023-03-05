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
        CardInfo? selectedCI;

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
                    //if (thumb.GetCard().GetType() == SelectedCI.MainView)
                    //{
                    //    CardInstances.Add(thumb);
                    //}
                }
            }

            PopOpen = Visibility.Visible;
        }

        //[RelayCommand]
        //void OpenInstalledCardPanel()
        //{
        //    PopOpen = Visibility.Visible;
        //}

        [RelayCommand]
        void CloseCard(MyThumb card)
        {
            var wvvm = App.GetService<WidgetViewVM>();
            wvvm.CloseCardCommand.Execute(card);


            GetCardDetail();
            //SelectedCard.GetCard().OnDisabled();
            //cv?.Children?.Remove(SelectedCard);
            //Config?.instances.Remove(SelectedCard.GetCard().GUID);
        }

        [RelayCommand]
        void SetCardLock(MyThumb? card)
        {
            card?.SetLocked();
        }
        [RelayCommand]
        void AddCardToDesktop(object? info)
        {
            App.GetService<WidgetViewVM>().CreateCard(info as CardInfo);


            GetCardDetail();
        }
        [RelayCommand]
        void ShowCardSetting(MyThumb? thumb)
        {
            //App.GetService<WidgetViewVM>().ShowCardSettingCommand(thumb);

        }
    }
}
