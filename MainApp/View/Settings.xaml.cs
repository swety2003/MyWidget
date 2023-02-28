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
