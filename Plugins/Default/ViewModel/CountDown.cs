using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Default.ViewModel
{
    partial class CountDown : ObservableRecipient
    {
        View.CountDown view;
        public CountDown(View.CountDown view)
        {
            this.view = view;
            IsActive = true;
            //view.cfg = Model.CountDown.Config.Load(view.GetPluginConfigFilePath());
            //_targetTime = view.cfg.Date;
            //EventName = view.cfg.Event;
        }
        [ObservableProperty]
        private string _eventName;

        private DateTime _targetTime;

        public DateTime TargetTime
        {
            get { return _targetTime; }
            set { SetProperty(ref _targetTime, value); }
        }

        [ObservableProperty]
        private string _leftTime;

        [ObservableProperty]
        private string _unit;


        //public void Receive(OnExitMsg message)
        //{
        //    view.cfg.Event = EventName;
        //    view.cfg.Date = TargetTime;
        //    view.cfg.Save(view.GetPluginConfigFilePath());
        //}

        public void Update()
        {
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = TargetTime - timeB; //计算时间差

            string time = "";
            if (ts.Days != 0)
            {
                time = ts.Days.ToString();
                Unit = "天";
            }
            else if (ts.Hours != 0)
            {
                time = ts.Hours.ToString();
                Unit = "时";
            }
            else if (ts.Minutes != 0)
            {
                time = ts.Minutes.ToString();
                Unit = "分";
            }
            else if (ts.Seconds != 0)
            {
                time = ts.Seconds.ToString();
                Unit = "秒";
            }
            else
            {
                time = "过期";

                Unit = "";
            }
            LeftTime = time;
        }
    }
}
