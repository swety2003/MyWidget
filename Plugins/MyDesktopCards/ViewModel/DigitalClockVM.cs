using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Threading;

namespace MyDesktopCards.ViewModel
{
    internal partial class DigitalClockVM : ObservableObject
    {
        [ObservableProperty]
        string _HourAndMinute;
        [ObservableProperty]
        string _Second;
        [ObservableProperty]
        string _Date;
        [ObservableProperty]
        string _Week;
        [ObservableProperty]
        string _Hello;

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;

                if (value)
                {
                    _Timer.Tick += _Timer_Tick;
                    _Timer.Start();
                }
                else
                {
                    _Timer.Tick -= _Timer_Tick;
                    _Timer.Stop();
                }
            }
        }


        DispatcherTimer _Timer = new DispatcherTimer { Interval = new TimeSpan(1000) };



        public string GetWeek(DateTime dt)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(dt.DayOfWeek)];

            return week;
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
        private void _Timer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Date = now.ToString("D");
            this.HourAndMinute = now.ToString("t");
            this.Second = $":{now.ToString("ss")}";
            this.Week = GetWeek(now);
            this.Hello = $"{GetNow()}{System.Environment.UserName}";
            TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);
        }
    }
}
