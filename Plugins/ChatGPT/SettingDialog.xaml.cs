using ChatGPT_GUI.ViewModels;
using System.Windows;

namespace ChatGPT_GUI
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog : Window
    {
        public SettingDialog(SettingViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
