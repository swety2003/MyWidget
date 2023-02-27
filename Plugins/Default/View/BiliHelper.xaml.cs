using Default.CWindow;
using Flurl;
using Flurl.Http;
using HandyControl.Controls;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static Default.Model.BiliHelper;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class BiliHelper : UserControl, ICard
    {
        public int HeightPix => 3;

        public int WidthPix => 5;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "哔哩哔哩", "描述", typeof(BiliHelper));



        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        ViewModel.BiliHelper vm;
        //String Cookie = "";
        public BiliHelper(Guid g)
        {
            InitializeComponent();

            GUID = g;

            vm = new ViewModel.BiliHelper(this);


        }

        public void OnEnabled()
        {
            DataContext = vm;
            vm.Loading = true;

            vm.IsActive = true;


            vm.cfg = Config.Load(this.GetPluginConfigFilePath());


            DataUpdate(false);

            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };

        }

        public void OnDisabled()
        {
            timer.Stop();
        }

        public static string GetMidFromCookie(string cookie)
        {
            List<string> DedeUserID = cookie.Split("; ").Where(x => x.IndexOf("DedeUserID=") != -1).ToList();
            if (DedeUserID.Count == 0)
            {
                return String.Empty;
            }
            else
            {
                return DedeUserID[0].Replace("DedeUserID=", "");
            }
        }
        /// <summary>
        /// http://api.bilibili.com/x/web-interface/card?mid=196435612&photo=true
        /// </summary>
        public async void DataUpdate(bool tip = true)
        {
            var Cookie = vm.cfg.cookie;
            if (Cookie != null && Cookie != "" && GetMidFromCookie(Cookie) != "")
            {
                try
                {
                    string api = "http://api.bilibili.com/x/web-interface/card";
                    string url = api.SetQueryParams(new { mid = GetMidFromCookie(Cookie), photo = "true" });
                    vm.Card = await url.GetJsonAsync<web_interface_card.Root>();
                    vm.Loading = false;

                    timer.Start();
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                    timer.Stop();
                }
            }
            else if (tip)
            {
                Growl.Error("哔哩哔哩:无效的Cookie!");
                vm.Loading = true;
                timer.Stop();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dl = new CookieGetter("https://passport.bilibili.com/login");
            dl.ShowDialog();
            vm.cfg.cookie = dl.Cookie;

            if (!String.IsNullOrEmpty( GetMidFromCookie(dl.Cookie)))
            {
                DataUpdate(true);

                vm.cfg.Save(this.GetPluginConfigFilePath());
                
            }
        }
    }
}
