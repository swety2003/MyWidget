using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.ProjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModel
{
    partial class ProjManagerVM:ObservableObject
    {
        [ObservableProperty]
        IProjM selectedType;


        public ProjManagerVM()
        {
            var p = new string[] { "D:\\Source\\Repos" };
            selectedType = new SlnManage(p);
            selectedType.Update();
        }

        [ObservableProperty]
        ProjInfo selctedInfo;

        [RelayCommand]
        void Open()
        {
            if (SelctedInfo == null)
            {
                return;
            }
            SelectedType?.Active(SelctedInfo);
        }
    }
}
