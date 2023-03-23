using MyWidgets.APP.ViewModel;
using MyWidgets.APP.Common;
using System.Windows.Controls;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// CardManage.xaml 的交互逻辑
    /// </summary>
    public partial class CardManage : Page, IPageFlags
    {
        public CardManage()
        {
            InitializeComponent();

            DataContext = App.GetService<CardManageVM>();
        }

        public PageFlags PageFlag => PageFlags.Root;
    }
}
