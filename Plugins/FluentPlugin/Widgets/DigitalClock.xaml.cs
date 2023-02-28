using CommunityToolkit.Mvvm.ComponentModel;
using FluentStyle.Utils;
using PluginSDK;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    /// 
    partial class DigitalClock : UserControl, ICard
    {
        public int HeightPix => 4;

        public int WidthPix => 4;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "时钟", "显示时间", typeof(DigitalClock));


        void ICard.OnEnabled()
        {
        }

        void ICard.OnDisabled()
        {
        }

        DigitalClockModel vm;
        private DispatcherTimer dt;
        public DigitalClock(Guid g)
        {
            InitializeComponent();
        }

        private void WidgetBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            vm = new DigitalClockModel();
            this.DataContext = vm;

            dt = new DispatcherTimer();

            dt.Interval = TimeSpan.FromSeconds(0.1);
            dt.Tick += dt_Tick;
            dt.Start();
        }



        private string GetNow()
        {
            int a = DateTime.Now.Hour;
            if (a < 6)
            {
                return "夜深了,";
            }
            else if (a < 8)
            {
                return "早上好,";
            }
            else if (a < 12)
            {
                return "上午好,";
            }
            else if (a < 13) { return "中午好,"; }
            else if (a < 18)
            {
                return "下午好,";
            }
            else
            {
                return "晚上好,";
            }
        }

        public string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];

            return week;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            vm.Date = now.ToString("D");
            vm.HourAndMinute = now.ToString("t");
            vm.Second = $":{now.ToString("ss")}";
            vm.Week = Week();
            vm.Hello = $"{GetNow()}{System.Environment.UserName}";
            TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);
            vm.StartTime = $"已开机 {m_WorkTimeTemp.Days}天 {m_WorkTimeTemp.Hours}时 {m_WorkTimeTemp.Minutes}分";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("确认关机?", "提示", MessageBoxButton.OKCancel);
            if (a == MessageBoxResult.OK)
            {
                BasicMethods.runcmd("shutdown -p");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("确认关机?", "提示", MessageBoxButton.OKCancel);
            if (a == MessageBoxResult.OK)
            {
                BasicMethods.runcmd("shutdown -p");

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("rundll32.exe user32.dll LockWorkStation");

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("确认重启?", "提示", MessageBoxButton.OKCancel);
            if (a == MessageBoxResult.OK)
            {
                BasicMethods.runcmd("shutdown -r now");

            }
        }
    }

    partial class DigitalClockModel : ObservableObject
    {
        [ObservableProperty]

        private string _hourAndMinute;


        [ObservableProperty]
        private string _startTime;




        [ObservableProperty]
        private string _hello;



        [ObservableProperty]
        private string _second;



        [ObservableProperty]
        private string _week;


        [ObservableProperty]
        private string _date;



    }
}
