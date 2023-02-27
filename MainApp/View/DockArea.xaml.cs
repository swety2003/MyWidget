using MainApp.Common;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainApp.View
{
    /// <summary>
    /// DockArea.xaml 的交互逻辑
    /// </summary>
    public partial class DockArea : Window
    {
        MainWindow mw;
        public DockArea(MainWindow f)
        {
            InitializeComponent();
            mw=f;
            Left = f.Left;
            Top = f.Top;
            Height= f.Height;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);


            var handle = new WindowInteropHelper(this).Handle;
            var father = new WindowInteropHelper(mw).Handle;

            PInvoke.SetFather(handle, father);

        }
    }
}
