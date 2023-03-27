using Flurl.Http;
using MyWidgets.APP.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, IPageFlags
    {
        public AboutPage()
        {
            InitializeComponent();

            ApplyBuildInfo();
        }

        public PageFlags PageFlag => PageFlags.Root;


        public void ApplyBuildInfo()
        {
            var asm= Assembly.GetExecutingAssembly();
            app_info.Text = asm.GetCustomAttribute<GitAttribute>()?.ToString();
            
        }

        const string repo = "https://github.lyric.today/repos/swety2003/MyWidget/actions/artifacts";

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckUpdate();
        }

        async Task CheckUpdate()
        {

            var asm = Assembly.GetExecutingAssembly();
            var self_hash = asm.GetCustomAttribute<GitAttribute>()??throw new System.Exception();


            try
            {

                switch (self_hash.GetRelType())
                {
                    case GitAttribute.RelType.NightlyBuild:
                        break;
                    //case GitAttribute.RelType.SelfBuild:
                    //    MessageBox.Show("该构建版本不支持检查更新！");
                    //    break;
                    //case GitAttribute.RelType.Release:
                    //    MessageBox.Show("该构建版本不支持检查更新！");
                    //    break;
                    //case GitAttribute.RelType.UnKnown:
                    //    MessageBox.Show("该构建版本不支持检查更新！");
                        //break;
                    default:
                        MessageBox.Show($"该构建版本({self_hash.GetRelType()})不支持检查更新！");
                        return;
                }

                var artifactsRsp = await repo.GetStringAsync();



                var artifacts = JsonConvert.DeserializeObject<WorkFlowRun.Root>(artifactsRsp).artifacts;

                var latest_run = artifacts.First();

                if (latest_run != null && self_hash != null)
                {
                    if (self_hash.Hash != latest_run.workflow_run.head_sha)
                    {
                        var downloadUrl = $"https://nightly.link/swety2003/MyWidget/actions/artifacts/{latest_run.id}.zip";

                        var ret = MessageBox.Show($"当前版本{self_hash}\r\n最新版本:{latest_run.workflow_run.head_branch}-{latest_run.workflow_run.head_sha}", "有新版本！");

                        if (ret == MessageBoxResult.OK)
                        {
                            Process.Start("explorer.exe", downloadUrl);
                        }
                    }
                    else
                    {
                        MessageBox.Show("当前已是最新版本！");
                    }

                }
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }
    }


    public class WorkFlowRun
    {
        public class Workflow_run
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string repository_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_repository_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_branch { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_sha { get; set; }
        }

        public class ArtifactsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string node_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string size_in_bytes { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string archive_download_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expired { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string created_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string updated_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expires_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Workflow_run workflow_run { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int total_count { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ArtifactsItem> artifacts { get; set; }
        }
    }
}
