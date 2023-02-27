using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DGP.Genshin.GamebarWidget.Model;

namespace Default.ViewModel
{
    partial class GenshinHelper : ObservableRecipient
    {
        View.GenshinHelper view;
        public GenshinHelper(View.GenshinHelper view)
        {
            this.view = view;
            IsActive = true;
            //view.cfg = Model.GenshinHelper.Config.Load(view.GetPluginConfigFilePath());

        }
        private RoleAndNote _roleAndNote;

        public RoleAndNote RoleAndNote
        {
            get { return _roleAndNote; }
            set { SetProperty(ref _roleAndNote, value); }
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        //public void Receive(OnExitMsg message)
        //{
        //    view.cfg.Save(view.GetPluginConfigFilePath());
        //}
    }
}
