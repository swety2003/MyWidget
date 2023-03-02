using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Projm.ProjectManager
{


    public interface IProjM : INotifyPropertyChanged
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjInfo> ProjInfos { get; set; }

        public Task Update();

        public void Active(ProjInfo selected);

        public void OpenInExplorer(ProjInfo selected);


        public void SetFolders(string[] folders);
    }

    public record ProjInfo(string name, string path, string ico);
}
