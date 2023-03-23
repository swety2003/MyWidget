using MyWidgets.APP.ViewModel;
using MyWidgets.APP.Common;
using System.Windows.Controls;

namespace MyWidgets.APP.View
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
