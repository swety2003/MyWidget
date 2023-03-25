using MyWidgets.APP.Common;
using MyWidgets.APP.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MyWidgets.APP.View
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



            App.GetService<SideBarManageService>().Container = sb_container;
            App.GetService<SideBarManageService>().ContainerPop = sb_container_pop;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //为了自动加载已启用的控件
            var vm = App.GetService<SideBarManageVM>();

            WinHide.Hide(this);


            //var handle = new WindowInteropHelper(this).Handle;
            //var father = new WindowInteropHelper(mw).Handle;

            //PInvoke.SetFather(handle, father);

#if DEBUG

            DesktopAppBar.SetAppBar(this, AppBarEdge.None);
            Width = 48;
            Topmost = true;
#endif

        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {

            new Settings().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //da.Close();
            DesktopAppBar.SetAppBar(this, AppBarEdge.None);
            this.Close();

            App.Current.Shutdown();
        }

        private void sb_container_pop_Closed(object sender, EventArgs e)
        {
            (sender as Popup).Child = null;
        }
    }
}
