using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ProjectManager
{


    public interface IProjM
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjInfo> ProjInfos { get; set; }

        public Task Update();

        public void Active(ProjInfo selected);

        public void SetFolders(string[] folders);
    }

    public record ProjInfo(string name, string path, string ico);
}
