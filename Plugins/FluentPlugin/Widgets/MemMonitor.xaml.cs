using CommunityToolkit.Mvvm.ComponentModel;
using PluginSDK;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MemMonitor : UserControl, ICard
    {
        public int HeightPix => 1;

        public int WidthPix => 4;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "内存占用", "描述", typeof(MemMonitor));


        void ICard.OnEnabled()
        {
        }

        void ICard.OnDisabled()
        {
        }
        private PerformanceCounter MEMCommitedPerc;
        private DispatcherTimer dt;
        private MemMonitorVM vm;



        public MemMonitor(Guid g)
        {
            InitializeComponent();
        }


        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new MemMonitorVM();
            this.DataContext = vm;
            MEMCommitedPerc = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dt_Tick;
            dt.Start();
        }
        public void getprocessorUtility()
        {
            vm.MemAvailable = MEMCommitedPerc.NextValue();
            vm.Text = Math.Round(vm.MemAvailable, 1).ToString();

        }
        void dt_Tick(object sender, EventArgs e)
        {
            getprocessorUtility();
        }
    }


    partial class MemMonitorVM : ObservableObject
    {
        [ObservableProperty]
        private float _memAvailable;


        [ObservableProperty]
        private string _text;




    }
}
