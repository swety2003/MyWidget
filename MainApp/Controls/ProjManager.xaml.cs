using MainApp.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainApp.Controls
{
    /// <summary>
    /// ProjManager.xaml 的交互逻辑
    /// </summary>
    public partial class ProjManager : UserControl
    {
        public ProjManager()
        {
            InitializeComponent();
            DataContext= new ProjManagerVM();
        }

        private void lb_ItemClick(object sender, RoutedEventArgs e)
        {
        }

        private void lb_MouseUp(object sender, MouseButtonEventArgs e)
        {

            content_pop.IsOpen = true;
            content_pop.Focus();
        }
    }
}
