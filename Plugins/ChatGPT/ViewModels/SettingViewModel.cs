using ChatGPT;
using ChatGPT.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using PluginSDK;
using System.ComponentModel;

namespace ChatGPT_GUI.ViewModels;
public partial class SettingViewModel:ObservableObject 
{

    public SettingViewModel(MainView view)
    {
        Appcfg = view.AppConfig;
        if (view.AppConfig.API_Key!=null)
        {

            ApiKey = view.AppConfig.API_Key;
        }

        this.view = view;
    }

    [ObservableProperty]
    string _apiKey;
    private readonly MainView view;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (_apiKey != null)
        {
            Appcfg.API_Key= _apiKey;

            Appcfg.Save(view.GetPluginConfigFilePath());
        }
    }

    public Config Appcfg { get; }



}
