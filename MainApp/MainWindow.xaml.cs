
using MainApp.Common;
using MainApp.View;
using PluginSDK;
using System;
using System.ComponentModel;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Interop;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Screen _screen;

        public MainWindow()
        {
            InitializeComponent();

        }

        DockArea? da;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var handle = new WindowInteropHelper(this).Handle;

            //_screen = Screen.PrimaryScreen;

            Height = SystemParameters.WorkArea.Height;
            Width = SystemParameters.WorkArea.Width;

            Left = 0;
            Top = 0;


            PInvoke.SetFather(handle);

            da = new DockArea(this);
            da.Show();

            frame.Navigate(App.GetService<WidgetView>());

            LoadSBI();

            LoadWindow();


        }



        void LoadSBI()
        {

            foreach (var item in App.GetService<PluginLoader>().SideBarItemInfos)
            {

                sb_container.Children.Add(Activator.CreateInstance(item.MainView) as UIElement);
            }
        }

        void LoadWindow()
        {
            //foreach (var item in App.GetService<PluginLoader>().WindowInfos)
            //{
            //    var w = Activator.CreateInstance(item.MainView) as Window;

            //    w?.Show();
            //    (w as IWindow)?.OnEnabled();
            //}
        }




        protected override void OnClosing(CancelEventArgs e)
        {

            base.OnClosing(e);
        }


        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {

            new Settings().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //da.Close();


            App.Current.Shutdown();
        }

    }
}
