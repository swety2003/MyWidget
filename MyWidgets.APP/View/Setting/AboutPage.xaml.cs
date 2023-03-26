using MyWidgets.APP.Common;
using System.Reflection;
using System.Windows.Controls;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, IPageFlags
    {
        public AboutPage()
        {
            InitializeComponent();

            ApplyBuildInfo();
        }

        public PageFlags PageFlag => PageFlags.Root;


        public void ApplyBuildInfo()
        {
            var asm= Assembly.GetExecutingAssembly();
            app_info.Text = asm.GetCustomAttribute<GitAttribute>()?.ToString();
            
        }
    }
}
