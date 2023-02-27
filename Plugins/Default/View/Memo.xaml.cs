using PluginSDK;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class Memo : UserControl, ICard
    {
        public int HeightPix => 3;

        public int WidthPix => 3;

        public Guid GUID { get; private set; }


        private ViewModel.Memo vm;
        public Memo(Guid g)
        {
            InitializeComponent();

            GUID = g;



        }


        private void SetCardSize(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            string size = mi.Header as string;
            var s = size.Split('×');
            if (s.Length == 2)
            {
                this.ResizeCard(int.Parse(s[0]) * 2, int.Parse(s[1]) * 2);
            }
            vm.cfg.size = new System.Drawing.Size(int.Parse(s[0]) * 2, int.Parse(s[1]) * 2);

        }

        public void OnEnabled()
        {
            vm = new ViewModel.Memo(this);
            DataContext = vm;
            this.ResizeCard(vm.cfg.size.Width, vm.cfg.size.Height);


        }

        public void OnDisabled()
        {

        }
    }
}
