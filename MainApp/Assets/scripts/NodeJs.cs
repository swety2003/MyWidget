using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainApp.ProjectManager;

public class NodeProjectM : IProjM
{
    public NodeProjectM()
    {
    }

    string[] baseFolder = { "D:/Source/Nodejs", };

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Icon { get; set; } = "/Assets/icon/node-app.ico";

    //= new BitmapImage(new Uri("/Assets/icon/vs-sln.ico",UriKind.Relative));

    public string Name { get; set; } = "NodeJS";

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
                    var f = Directory.GetDirectories(d);

                    //if (f.Select((s) => s.ToLower() == ".idea").Count() != 0)
                    //{
                        projInfos.Add(new ProjInfo(Path.GetFileName(d), d, "/Assets/icon/python-py.ico"));
                    //}
                }
            }

            ProjInfos = projInfos;

            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(ProjInfos)));
        });
    }

    public void Active(ProjInfo selected)
    {
        Process.Start("explorer.exe", selected.path);
    }

    public void SetFolders(string[] folder)
    {
        baseFolder = folder;
    }

    public void OpenInExplorer(ProjInfo selected)
    {
        Process.Start("explorer.exe", Path.GetFullPath(selected.path));

    }
}
