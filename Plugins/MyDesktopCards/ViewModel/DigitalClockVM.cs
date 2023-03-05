using CommunityToolkit.Mvvm.ComponentModel;
using MyDesktopCards.Common;
using System;
using System.Windows.Threading;

namespace MyDesktopCards.ViewModel
{
    internal partial class DigitalClockVM : SimpleVM
    {

        [ObservableProperty]
        DateTime _NowTime;

        [ObservableProperty]
        string _Hello;

        public DigitalClockVM()
        {
            _Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
            this.OnActiveChanged += DigitalClockVM_OnActiveChanged;

        }

        private void DigitalClockVM_OnActiveChanged(object? sender, bool e)
        {
            if (e)
            {
                _Timer.Start();
                _Timer.Tick += _Timer_Tick;
            }
            else
            {
                _Timer.Stop();

                _Timer.Tick -= _Timer_Tick;

            }
        }


        public string GetWeek(DateTime dt)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(dt.DayOfWeek)];

            return week;
        }
        private string GetNow(DateTime dt)
        {
            int a = dt.Hour;
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
            NowTime = DateTime.Now;
            //this.Date = now.ToString("D");
            //this.HourAndMinute = now.ToString("t");
            //this.Second = $":{now.ToString("ss")}";
            //this.Week = GetWeek(now);
            this.Hello = $"{GetNow(NowTime)}{System.Environment.UserName}";

            TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);
        }
    }
}
