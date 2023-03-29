using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWidgets.APP.Common;
using MyWidgets.APP.Model;
using MyWidgets.SDK;
using MyWidgets.SDK.Controls;
using MyWidgets.SDK.Core.Card;

namespace MyWidgets.APP.ViewModel
{
    public partial class WidgetViewVM : ObservableObject
    {

        private AppConfig Config => App.GetService<AppConfigManager>().Config;
        //[ObservableProperty]
        //private ObservableCollection<UIElement> activeCards = new ObservableCollection<UIElement>();

        private CardManageService CardManagerService = App.GetService<CardManageService>();

        public WidgetViewVM()
        {
        }


        public void CreateCard(CardInfo? ci)
        {
            if (ci == null)
            {
                return;
            }

            CardManagerService.Create(ci);

            //App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);


        }



        // 被 CardControl 使用

        //[RelayCommand]
        //void CloseCard(object o)
        //{

        //    var thumb = o as CardControl;
        //    CardManagerService.Remove((thumb.GetCard().GUID));

        //    //App.GetService<CardManageVM>().GetCardDetailCommand.Execute(null);
        //}

        //[RelayCommand]
        //void LockCard(object o)
        //{
        //    var thumb = o as CardControl;
        //    if (thumb != null)
        //    {
        //        var r = thumb.SetLocked();
        //        Config.CardInstances[thumb.GetCard().GUID].Locked = r;
        //    }
        //}


        //[RelayCommand]
        //void ShowCardSetting(CardControl? thumb)
        //{
        //    thumb?.GetCard().ShowSetting();
        //}
    }
}
