using MainApp.Common;
using MainApp.ViewModel;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// SideBarManage.xaml 的交互逻辑
    /// </summary>
    public partial class SideBarManage : Page, IPageFlags
    {
        public SideBarManage()
        {
            InitializeComponent();

            DataContext = App.GetService<SideBarManageVM>();
        }

        public PageFlags PageFlag => PageFlags.Root;
    }
}
