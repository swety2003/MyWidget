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


        [ObservableProperty]
        private double hourDeg = 0;

        [ObservableProperty]
        private double minDeg = 0;

        [ObservableProperty]
        private double secondDeg = 0;


        private void _Timer_Tick(object? sender, EventArgs e)
        {
            NowTime = DateTime.Now;

            this.Hello = $"{GetNow(NowTime)}{System.Environment.UserName}";

            TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);



            HourDeg = _NowTime.Hour * 30 + _NowTime.Minute * 30 / 60 - 90;

            MinDeg = _NowTime.Minute * 6 + _NowTime.Second * 6 / 60 - 90;

            SecondDeg = _NowTime.Second * 6 - 90;
        }
    }
}
