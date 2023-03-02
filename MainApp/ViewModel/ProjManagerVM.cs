using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.ProjectManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using System.Collections;

namespace MainApp.ViewModel
{
    partial class ProjManagerVM:ObservableObject
    {
        [ObservableProperty]
        IProjM selectedType;

        [ObservableProperty]
        ObservableCollection<IProjM> projMs = new ObservableCollection<IProjM>();

        public ProjManagerVM()
        {
            //projMs.Add(new PycharmManage());
            //projMs.Add(new SlnManage());

            SelfLoadedCommand = new AsyncRelayCommand(LoadScript);
        }

        public AsyncRelayCommand SelfLoadedCommand { get; set; }


        public async Task LoadScript()
        {
            
            var tempList = new List<IProjM>();
            var scriptFolder = "Assets/scripts";
            if (Directory.Exists(scriptFolder))
            {
                var scripts = Directory.GetFiles(scriptFolder).Where((p) => p.ToLower().EndsWith(".cs")).ToList();
                foreach (var script in scripts)
                {
                    //await Task.Delay(1000);

                    List<MetadataReference> refs = new List<MetadataReference>();

                    var a = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var asmName in a)
                    {
                        try
                        {

                            refs.Add(MetadataReference.CreateFromFile(asmName.Location));
                        }
                        catch { }
                    }

                    var compilation = CSharpCompilation.Create(
                                                    null,
                                                    new[] { CSharpSyntaxTree.ParseText(File.ReadAllText(script)) },
                                                    refs,
                                                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                    using var memSteam = new MemoryStream();

                    var emitResult = compilation.Emit(memSteam);

                    if (!emitResult.Success )
                    {
                        continue;
                    }

                    memSteam.Seek(0, SeekOrigin.Begin);

                    var asm = Assembly.Load(memSteam.ToArray());

                    Type[] types = asm.GetTypes();
                    foreach (var t in types)
                    {
                        if (t.GetInterface("IProjM") != null)
                        {
                            var obj = (IProjM)Activator.CreateInstance(t);

                            if (obj != null)
                            {
                                tempList.Add(obj);
                            }
                        }
                    }

                }
            }
            foreach (var item in tempList)
            {
                ProjMs.Add(item);
            }
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

        [RelayCommand]
        void SelChange()
        {
            if (SelectedType == null)
            {
                return;
            }
            SelectedType.Update();
        }

        [RelayCommand]
        void OpenInExplorer() 
        {

            SelectedType.OpenInExplorer(SelctedInfo);

        }
    }
}
