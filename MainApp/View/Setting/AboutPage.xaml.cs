using MainApp.Common;
using System.Windows.Controls;

namespace MainApp.View
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, IPageFlags
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public PageFlags PageFlag => PageFlags.Root;
    }
}
