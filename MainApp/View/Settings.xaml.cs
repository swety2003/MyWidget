using MainApp.ViewModel;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainApp.View
{
    /// <summary>
    /// CardManage.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);


            fm?.Navigate(App.GetService<CardManage>());
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;

            var i = lb.SelectedIndex;

            switch (i)
            {
                case 0:
                    fm?.Navigate(App.GetService<CardManage>()); break;
                default:
                    break;
            }

        }
    }
}
