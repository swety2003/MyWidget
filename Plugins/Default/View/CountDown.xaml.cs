
using PluginSDK;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class CountDown : UserControl, ICard
    {
        public static CardInfo info = new CardInfo(null, "倒计时", "描述", typeof(CountDown));


        public int HeightPix => 3;

        public int WidthPix => 3;

        public Guid GUID { get; private set; }

        internal Model.CountDown.Config cfg;

        ViewModel.CountDown vm;
        private DispatcherTimer timer;

        public CountDown(Guid g)
        {
            InitializeComponent();

            GUID = g;


        }


        public void OnEnabled()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            vm = new ViewModel.CountDown(this);
            DataContext = vm;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            vm.Update();
        }

        public void OnDisabled()
        {
            timer.Stop();
            try
            {

                File.Delete(this.GetPluginConfigFilePath());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
