using PluginSDK;
using Projm.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projm.Controls
{
    /// <summary>
    /// ProjManager.xaml 的交互逻辑
    /// </summary>
    public partial class ProjManager : UserControl, ISideBarItem
    {

        public static SideBarItemInfo info = new SideBarItemInfo("1", "好", typeof(ProjManager));

        public ProjManager()
        {
            InitializeComponent();

            DataContext = new ProjManagerVM();
        }

        public void OnDisabled()
        {

        }

        public void OnEnabled()
        {

        }


        private void lb_MouseUp(object sender, MouseButtonEventArgs e)
        {

            content_pop.IsOpen = true;
            content_pop.Focus();
            tb.Focus();

        }

        private void content_pop_Closed(object sender, System.EventArgs e)
        {

        }
    }
}
