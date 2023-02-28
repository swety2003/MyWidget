using Flurl.Http;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class AISchedule : UserControl, ICard
    {
        public int HeightPix => 5;

        public int WidthPix => 5;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "小爱课程表", "描述", typeof(AISchedule));


        public AISchedule(Guid g)
        {
            InitializeComponent();

            GUID = g;

        }

        ViewModel.AISchedule vm;

        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };

        public class sectionTime
        {
            public int i { get; set; }
            /// <summary>
            /// 开始          
            /// </summary>
            public string s { get; set; }
            /// <summary>
            /// 结束
            /// </summary>
            public string e { get; set; }
        }

        public static List<sectionTime> sections = new List<sectionTime>();


        public void OnEnabled()
        {
            vm = new ViewModel.AISchedule(this);
            DataContext = vm;
            timer.Start();

            DataUpdate(false);
            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };

        }

        private void DataUpdate(bool v = false)
        {
            vm.LoadTable();
        }

        public void OnDisabled()
        {
            timer.Stop();

            if (File.Exists(this.GetPluginConfigFilePath()))
            {
                File.Delete(this.GetPluginConfigFilePath());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                var strPath = url.Text.Substring(url.Text.IndexOf("linkToken=") + "linkToken=".Length);

                byte[] bpath = Convert.FromBase64String(strPath);
                var linkToken = System.Text.ASCIIEncoding.Default.GetString(bpath);

                linkToken = System.Web.HttpUtility.UrlDecode(linkToken);

                var tokens = linkToken.Split('&');
                var a = $"https://i.ai.mi.com/course-multi/table?ctId={tokens[4]}&userId={tokens[0]}&deviceId={tokens[1]}&sourceName=course-app-browser";

                Task.Run(async () =>
                {
                    var t = this.GetPluginConfigFilePath();
                    var tf = Path.GetDirectoryName(t);
                    if (!Directory.Exists(tf))
                    {
                        Directory.CreateDirectory(tf);
                    }

                    await a.DownloadFileAsync(tf, Path.GetFileName(t));
                    vm.LoadTable();
                });
            }
            catch (Exception ex)
            {

                //Growl.Error(ex.Message);

            }


        }
    }
}
