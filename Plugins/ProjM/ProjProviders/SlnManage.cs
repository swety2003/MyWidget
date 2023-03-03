using ProjM;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

class SlnManage : IProjM
{

    static string selfp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
    public SlnManage()
    {

    }

    string[] baseFolder = { "D:\\Source\\Repos" };

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetFolders(string[] folders)
    {
        baseFolder = folders;
    }

    public string Icon { get; set; } = Path.Combine(selfp, "Assets/icon/vs-app.ico");
    //= new BitmapImage(new Uri("/Assets/icon/vs-sln.ico",UriKind.Relative));

    public string Name { get; set; } = "VisualStudio2022";

    public IEnumerable<ProjInfo> ProjInfos { get; set; }

    public async Task Update()
    {
        await Task.Run(() =>
        {
            List<ProjInfo> projInfos = new List<ProjInfo>();
            foreach (var item in baseFolder)
            {
                var di = Directory.GetDirectories(item);
                foreach (var d in di)
                {
                    var f = Directory.GetFiles(d);

                    foreach (var i in f)
                    {
                        if (i.ToLower().EndsWith(".sln"))
                        {
                            projInfos.Add(new ProjInfo(Path.GetFileName(i), i, Path.Combine(selfp, "Assets/icon/vs-sln.ico")));

                            break;
                        }
                    }
                }
            }

            ProjInfos = projInfos;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjInfos)));
        });
    }

    public void Active(ProjInfo selected)
    {
        Process.Start("explorer.exe", selected.path);
    }

    public void OpenInExplorer(ProjInfo selected)
    {
        Process.Start("explorer.exe", Path.GetDirectoryName(selected.path));

    }
}


