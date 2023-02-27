
using CommunityToolkit.Mvvm.ComponentModel;
using DefaultWidgets.Utils;
using PluginSDK;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static DefaultWidgets.Utils.SysBatteryInfo;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryInfo : UserControl,ICard
    {
        public int HeightPix => 2;

        public int WidthPix => 4;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "电量", "显示当前电量", typeof(BatteryInfo));


        void ICard.OnEnabled()
        {
        }

        void ICard.OnDisabled()
        {
        }

        private SysBatteryInfo batteryInfo;
        private DispatcherTimer dt;
        private BatteryInfoVM vm;


        public BatteryInfo(Guid g)
        {
            InitializeComponent();
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            batteryInfo = new SysBatteryInfo();
            vm = new BatteryInfoVM();
            this.DataContext = vm;
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(60);
            dt.Tick += dt_Tick;
            dt.Start();
            dt_Tick(null, null);

            //WWindow.UseDefaultCard(true);

        }

        void dt_Tick(object sender, EventArgs e)
        {
            SystemPowerStatus systemPowerStatus = batteryInfo.Get();
            vm.Value = systemPowerStatus.BatteryLifePercent;
            vm.Icon = systemPowerStatus.ACLineStatus == ACLineStatus_.Online ? "\uea93" : "\ue83f";
            vm.Text = vm.Value.ToString();
        }

    }
    partial class BatteryInfoVM : ObservableObject
    {
        [ObservableProperty]
        private int _value;

        [ObservableProperty]
        private string _icon;

        [ObservableProperty]

        private string _text;


    }
}
