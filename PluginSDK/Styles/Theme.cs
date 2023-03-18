using System;
using System.Windows;
using System.Windows.Markup;

namespace PluginSDK.Styles
{
    [Localizability(LocalizationCategory.Ignore)]
    [Ambient]
    [UsableDuringInitialization(true)]
    public class Theme : ResourceDictionary
    {
        public static Theme? Instance;
        public Theme()
        {
            Instance = this;
        }

        private ThemeType _type;

        public ThemeType Type
        {
            get => _type;
            set
            {
                switch (value)
                {
                    //浅色主题
                    case ThemeType.Light:
                        this.Source = new Uri($"pack://application:,,,/PluginSDK;component/Styles/NewBrush/LightBrush.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Dark:
                        //深色主题
                        this.Source = new Uri($"pack://application:,,,/PluginSDK;component/Styles/NewBrush/DarkBrush.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Other:


                        this.Source = new Uri($"pack://application:,,,/PluginSDK;component/Styles/NewBrush/DarkBrush.xaml", UriKind.Absolute);

                        break;
                }
                _type = value;
            }
        }

        public static void SetTheme(ThemeType themeType)
        {
            Application.Current.Resources.MergedDictionaries[1].MergedDictionaries.Remove(Instance);
            Application.Current.Resources.MergedDictionaries[1].MergedDictionaries.Add(new Theme { Type = themeType });
        }
    }


    public enum ThemeType
    {
        Light, Dark, Other
    }

}
