using PluginSDK;
using Projm.ViewModel;
using ProjM.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Projm.Controls
{
    /// <summary>
    /// ProjManager.xaml 的交互逻辑
    /// </summary>
    public partial class ProjManager : UserControl, ISideBarItem
    {

        public static SideBarItemInfo info = new SideBarItemInfo("项目管理器", "基于lua脚本自定义扫描规则的项目管理", typeof(ProjManager));

        public Guid GUID => throw new NotImplementedException();

        public Popup Popup { get; }

        public SideBarItemInfo Info => info;

        ProjManagerVM vm = new ProjManagerVM();

        public ProjManager(Popup pop)
        {
            InitializeComponent();

            DataContext = vm;

            Popup = pop;
        }

        public void OnDisabled()
        {

        }

        public void OnEnabled()
        {

        }

        private PopupView popupView = new PopupView();
        private void lb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            popupView.DataContext = vm;
            this.Show(popupView);

        }

        public void ShowSetting()
        {
            throw new NotImplementedException();
        }
    }
}
