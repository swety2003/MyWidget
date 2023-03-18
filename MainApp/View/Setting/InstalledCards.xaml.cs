using MainApp.Common;
using MainApp.ViewModel;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// InstalledCards.xaml 的交互逻辑
    /// </summary>
    public partial class InstalledCards : Page, IPageFlags
    {
        public InstalledCards(InstalledCardsVM vm)
        {
            InitializeComponent();

            DataContext = vm;

        }

        public PageFlags PageFlag => PageFlags.Sub;
    }
}
