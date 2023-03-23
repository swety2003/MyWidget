using MyWidgets.APP.ViewModel;
using MyWidgets.APP.Common;
using System.Windows.Controls;

namespace MyWidgets.APP.View
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
