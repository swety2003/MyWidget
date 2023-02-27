
using CommunityToolkit.Mvvm.ComponentModel;
using static Default.Model.BiliHelper;

namespace Default.ViewModel
{
    internal class BiliHelper : ObservableRecipient
    {

        public Model.BiliHelper.Config cfg;
        View.BiliHelper view;
        public BiliHelper(View.BiliHelper view)
        {
            this.view = view;
        }



        private space_acc_info.Root _acc_Info;

        public space_acc_info.Root Acc_Info
        {
            get { return _acc_Info; }
            set { SetProperty(ref _acc_Info, value); }
        }

        private space_myinfo.Root _space_Myinfo;

        public space_myinfo.Root Space_Myinfo
        {
            get { return _space_Myinfo; }
            set { SetProperty(ref _space_Myinfo, value); }
        }

        private web_interface_card.Root _card;

        public web_interface_card.Root Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        private string _cookie;

        //public string Cookie
        //{
        //    get { return _cookie; }
        //    set { SetProperty(ref _cookie, value); }
        //}


        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        //public void Receive(OnExitMsg message)
        //{
        //    cfg.Save(view.GetPluginConfigFilePath());

        //}
    }

}
