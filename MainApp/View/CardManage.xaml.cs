using MainApp.ViewModel;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// CardManage.xaml 的交互逻辑
    /// </summary>
    public partial class CardManage : Page
    {
        public CardManage()
        {
            InitializeComponent();

            DataContext = App.GetService<CardManageVM>();
        }
    }
}
