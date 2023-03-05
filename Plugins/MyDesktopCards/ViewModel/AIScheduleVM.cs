using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyDesktopCards.Common;
using MyDesktopCards.Model;
using MyDesktopCards.View;
using Newtonsoft.Json;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static MyDesktopCards.Model.ClassTableData;

namespace MyDesktopCards.ViewModel
{
    internal partial class AIScheduleVM: SimpleVM
    {
        private AISchedule view;
        private DateTime targetTime;

        [ObservableProperty]
        private List<CoursesItem> _cI;

        [ObservableProperty]
        private string week;

        [ObservableProperty]
        private string day;

        [ObservableProperty]
        private string tableTip;

        private bool IsIn(string[] a, string b)
        {
            Console.WriteLine(a);
            foreach (string a2 in a)
            {
                if (a2 == b) { return true; }
            }
            return false;
        }
        public int Week2Int(DayOfWeek w)
        {
            var a = Convert.ToInt32(w);
            if (a == 0)
            {
                return 7;
            }
            else
            {
                return a;
            }
        }



        public Setting setting { get; set; }

        public AIScheduleVM(AISchedule view)
        {
            this.view = view;
            OnActiveChanged += AIScheduleVM_OnActiveChanged;

            targetTime = DateTime.Now;
        }

        private void AIScheduleVM_OnActiveChanged(object? sender, bool e)
        {
            if (e)
            {
                LoadTable();
                _Timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
                _Timer.Start();
                _Timer.Tick += _Timer_Tick;
            }
            else
            {
                _Timer.Tick-= _Timer_Tick;
                _Timer.Stop();
            }
        }

        private void _Timer_Tick(object? sender, EventArgs e)
        {
            LoadTable();
        }

        public List<sectionTime> AllSections { get; private set; }= new List<sectionTime>();

        public async void LoadTable()
        {
            try
            {

                string table = await File.ReadAllTextAsync(view.GetPluginConfigFilePath());
                TableRoot tr = JsonConvert.DeserializeObject<TableRoot>(table);
                setting = tr.data.setting;

                AllSections = JsonConvert.DeserializeObject<List<ClassTableData.sectionTime>>(setting.sectionTimes);

                var week = Update();

                CI = tr.data.courses.Where(x => IsIn(x.weeks.Split(','), week) && x.day == Week2Int(targetTime.DayOfWeek)).ToList();

                if (CI.Count == 0)
                {
                    TableTip = "今日无课";
                }
                else
                {
                    TableTip = "";
                }

            }
            catch (Exception ex)
            {

                //Growl.Error(ex.Message);
                TableTip = "未设置课表";

            }

        }
        public string Week2CN()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(targetTime.DayOfWeek)];

            return week;
        }
        private string Update()
        {
            string Start = setting.startSemester;
            DateTime start = StampToDateTime(Start);
            TimeSpan ts = targetTime - start;
            int week = (int)Math.Floor((double)ts.Days / 7) + 1;
            Week = $"第 {week} 周 ";
            Day = $"{Week2CN()}";
            return week.ToString();
        }

        public static DateTime StampToDateTime(string timeStamp)
        {


            long timestamp = long.Parse(timeStamp);

            if (timeStamp.Length==13)
            {
                timestamp /= 1000;
            }

            System.DateTime startTime = new System.DateTime(1970, 1, 1);
            var time = startTime.AddSeconds(timestamp);
            return time;
        }

        [RelayCommand]
        private void Previous()
        {
            targetTime = targetTime.AddDays(-1);
            LoadTable();
        }

        [RelayCommand]
        private void Next()
        {
            targetTime = targetTime.AddDays(+1);
            LoadTable();
        }


    }
}
