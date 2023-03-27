using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using MyWidgets.APP.View;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MyWidgets.APP.ViewModel
{
    internal partial class AboutPageVM : ObservableObject
    {

        const string repo = "https://github.lyric.today/repos/swety2003/MyWidget/actions/artifacts";

        public AsyncRelayCommand CheckUpdateCommand => new AsyncRelayCommand(CheckUpdate);

        public AboutPageVM()
        {
            SetBuildInfo();
        }

        async Task CheckUpdate()
        {

            var asm = Assembly.GetExecutingAssembly();
            var self_hash = asm.GetCustomAttribute<GitAttribute>() ?? throw new System.Exception();


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

                        var ret = MessageBox.Show($"当前版本{self_hash}\r\n最新版本:{latest_run.workflow_run.head_branch}-{latest_run.workflow_run.head_sha}", "有新版本！", MessageBoxButton.OKCancel);

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

        [ObservableProperty]
        string appInfo;

        private void SetBuildInfo()
        {

            var asm = Assembly.GetExecutingAssembly();
            AppInfo = asm.GetCustomAttribute<GitAttribute>()?.ToString();
        }
    }
}
