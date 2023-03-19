using MainApp.Common;
using MainApp.Model;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace MainApp.View
{
    /// <summary>
    /// DockArea.xaml 的交互逻辑
    /// </summary>
    public partial class DockArea : Window
    {
        AppConfig config=> App.GetService<AppConfigManager>().Config;

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


            //var handle = new WindowInteropHelper(this).Handle;
            //var father = new WindowInteropHelper(mw).Handle;

            //PInvoke.SetFather(handle, father);
            var sbi = App.GetService<PluginLoader>().SideBarItemInfos;

            foreach (var item in config.EnabledSideBarItems)
            {
                foreach (var info in sbi)
                {
                    if (info.MainView.FullName==item)
                    {
                        App.GetService<SideBarManageService>().Create(info,true);
                    }
                }
            }
        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {

            new Settings().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //da.Close();
            Common.DesktopAppBar.SetAppBar(this, AppBarEdge.None);
            this.Close();

            App.Current.Shutdown();
        }

        private void sb_container_pop_Closed(object sender, EventArgs e)
        {
            (sender as Popup).Child = null;
        }
    }
}
