using MainApp.Common;
using MainApp.View.Setting;
using System;
using System.Windows;
using System.Windows.Controls;

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


            var ns = App.GetService<NavigationService>();

            ns.Init(this);

            ns?.NavigateTo(App.GetService<CardManage>());

            lb.SelectedIndex = 0;

        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;

            var i = lb.SelectedIndex;

            var ns = App.GetService<NavigationService>();

            switch (i)
            {
                case 0:
                    ns.NavigateTo(App.GetService<CardManage>()); break;
                case 1:
                    ns.NavigateTo(App.GetService<SideBarManage>()); break;
                case 2:
                    ns.NavigateTo(App.GetService<PreferencePage>()); break;
                case 3:
                    ns.NavigateTo(App.GetService<AboutPage>()); break;


                default:
                    break;
            }

        }
    }
}
