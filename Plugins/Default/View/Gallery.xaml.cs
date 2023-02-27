using PluginSDK;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Default.View
{
    /// <summary>
    /// Gallery.xaml 的交互逻辑
    /// </summary>
    public partial class Gallery : UserControl, ICard
    {
        public static CardInfo info = new CardInfo(null, "测试卡片", "描述", typeof(Gallery));

        public int HeightPix => 4;

        public int WidthPix => 4;

        public Guid GUID { get; private set; }
    
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 10) };

        ViewModel.Gallery vm;
        public Gallery(Guid g)
        {
            InitializeComponent();
            GUID = g;
        }

        public void OnEnabled()
        {


            vm = new ViewModel.Gallery(this);
            vm.IsActive = true;
            DataContext = vm;

            this.ResizeCard(vm.cfg.size.Width, vm.cfg.size.Height);

            vm.InitFolder();



            timer.Tick += (object? sender, EventArgs e) =>
            {
                vm.Next();
            };
            timer.Start();
            //throw new NotImplementedException();
        }

        public void OnDisabled()
        {
            vm.IsActive = false;

            //throw new NotImplementedException();
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
    }
}
