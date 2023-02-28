using MainApp.Common;
using System;
using System.Windows;
using System.Windows.Interop;

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
            mw = f;
            Left = f.Left;
            Top = f.Top;
            Height = f.Height;
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
