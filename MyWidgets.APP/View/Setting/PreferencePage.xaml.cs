using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesign3.Styles.Colors;
using MyWidgets.APP.Common;
using MyWidgets.APP.Model;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MyWidgets.APP.View.Setting
{
    /// <summary>
    /// PreferencePage.xaml 的交互逻辑
    /// </summary>
    public partial class PreferencePage : Page, IPageFlags
    {

        public PreferencePage()
        {
            InitializeComponent();

            DataContext = App.GetService<PreferenceVM>();
        }

        public PageFlags PageFlag => PageFlags.Root;
    }


    public partial class PreferenceVM : ObservableObject
    {

        AppConfig config => App.GetService<AppConfigManager>().Config;

        public PreferenceVM()
        {
            SelectedThemeIndex = AllTheme.ToList().IndexOf(config.ThemeType.ToString());
        }

        [ObservableProperty]
        int selectedThemeIndex = 0;

        public string[] AllTheme => Enum.GetNames(typeof(ThemeType));


        [RelayCommand]
        void SaveConfig()
        {
            config.ThemeType = Enum.Parse<ThemeType>(AllTheme[SelectedThemeIndex]);
            App.SetTheme(config.ThemeType);
        }


    }
}
