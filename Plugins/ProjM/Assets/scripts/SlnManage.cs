using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainApp.ProjectManager;

class SlnManage: IProjM
{
    public SlnManage()
    {

    }

    string[] baseFolder = { "D:\\Source\\Repos" };

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetFolders(string[] folders)
    {
        baseFolder = folders;
    }

    public string Icon { get; set; }= "/Assets/icon/vs-app.ico";
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
                            projInfos.Add(new ProjInfo(Path.GetFileName(i), i, "Assets/icon/vs-sln.ico"));

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


