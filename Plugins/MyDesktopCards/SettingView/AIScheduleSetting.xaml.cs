using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            DataContext = new AIScheduleSettingVM(card);
        }


    }

    public partial class AIScheduleSettingVM:ObservableObject
    {
        [ObservableProperty]
        string shareUrl = "";
        private ICard card;

        public AIScheduleSettingVM(ICard card)
        {

            this.card = card;

            ImportTableCommand = new AsyncRelayCommand(ImportTable);
        }

        public AsyncRelayCommand ImportTableCommand { get; set; }


        async Task ImportTable()
        {
            try
            {
                var strPath = ShareUrl.Substring(ShareUrl.IndexOf("linkToken=") + "linkToken=".Length);

                byte[] bpath = Convert.FromBase64String(strPath);
                var linkToken = System.Text.ASCIIEncoding.Default.GetString(bpath);

                linkToken = System.Web.HttpUtility.UrlDecode(linkToken);

                var tokens = linkToken.Split('&');
                var a = $"https://i.ai.mi.com/course-multi/table?ctId={tokens[4]}&userId={tokens[0]}&deviceId={tokens[1]}&sourceName=course-app-browser";

                var t = (card as UserControl).GetPluginConfigFilePath();
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

    }
}
