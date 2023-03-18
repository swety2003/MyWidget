using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using PluginSDK;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace MyDesktopCards.SettingView
{
    /// <summary>
    /// AIScheduleSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AIScheduleSetting : Window
    {
        private Guid guid;

        public AIScheduleSetting(ICard card)
        {
            InitializeComponent();
            DataContext = new AIScheduleSettingVM(card, this);
        }


    }

    public partial class AIScheduleSettingVM : ObservableObject
    {
        ILogger<AIScheduleSettingVM> logger;

        [ObservableProperty]
        string shareUrl = "";
        private ICard card;
        AIScheduleSetting view;

        public AIScheduleSettingVM(ICard card, AIScheduleSetting view)
        {

            this.card = card;
            this.view = view;

            ApplySettingCommand = new AsyncRelayCommand(ApplySettings);

            logger = Logger.CreateLogger<AIScheduleSettingVM>();
        }

        //public AsyncRelayCommand ImportTableCommand { get; set; }


        async Task ImportTable()
        {
            if (String.IsNullOrEmpty(ShareUrl))
            {
                return;
            }
            try
            {
                var strPath = ShareUrl.Substring(ShareUrl.IndexOf("linkToken=") + "linkToken=".Length);

                byte[] bpath = Convert.FromBase64String(strPath);
                var linkToken = System.Text.ASCIIEncoding.Default.GetString(bpath);

                linkToken = System.Web.HttpUtility.UrlDecode(linkToken);

                var tokens = linkToken.Split('&');
                var a = $"https://i.ai.mi.com/course-multi/table?ctId={tokens[4]}&userId={tokens[0]}&deviceId={tokens[1]}&sourceName=course-app-browser";

                var t = card.GetPluginConfigFilePath();
                var tf = Path.GetDirectoryName(t);
                if (!Directory.Exists(tf))
                {
                    Directory.CreateDirectory(tf);
                }

                await a.DownloadFileAsync(tf, Path.GetFileName(t));

                MessageBox.Show("导入完成！");


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }


        public AsyncRelayCommand ApplySettingCommand { get; set; }

        async Task ApplySettings()
        {
            await ImportTable();

        }

        [ObservableProperty]
        string overrideUIFile = "";

       


    }
}
