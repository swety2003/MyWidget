using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Default.ViewModel
{
    internal class Memo : ObservableRecipient
    {
        public Model.Memo.Config cfg;

        View.Memo view;
        public Memo(View.Memo view)
        {
            this.view = view;
            IsActive = true;
            //cfg = Model.Memo.Config.Load(view.GetPluginConfigFilePath());
        }


        public string Text
        {
            get { return cfg.text; }
            set { SetProperty(ref cfg.text, value); }
        }


        //public void Receive(OnExitMsg message)
        //{
        //    cfg.Save(view.GetPluginConfigFilePath());

        //}
    }
}
