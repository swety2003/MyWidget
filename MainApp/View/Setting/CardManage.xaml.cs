using MainApp.Common;
using MainApp.ViewModel;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// CardManage.xaml 的交互逻辑
    /// </summary>
    public partial class CardManage : Page,IPageFlags
    {
        public CardManage()
        {
            InitializeComponent();

            DataContext = App.GetService<CardManageVM>();
        }

        public PageFlags PageFlag => PageFlags.Root;
    }
}
