using MaterialColorUtilities.Schemes;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyWidgets.SDK.Styles
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
                        this.Source = new Uri($"pack://application:,,,/MyWidgets.SDK;component/Styles/NewBrush/LightBrush.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Dark:
                        //深色主题
                        this.Source = new Uri($"pack://application:,,,/MyWidgets.SDK;component/Styles/NewBrush/DarkBrush.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.DynamicDark:

                        GenerateColorFormImage(true);
                        break;
                    case ThemeType.DynamicLight:

                        GenerateColorFormImage();
                        break;
                    default:
                        this.Source = new Uri($"pack://application:,,,/MyWidgets.SDK;component/Styles/NewBrush/DarkBrush.xaml", UriKind.Absolute);

                        break;
                }
                _type = value;
            }
        }

        public static void SetTheme(ThemeType themeType)
        {
            //Application.Current.Resources.MergedDictionaries[1].MergedDictionaries.Remove(Instance);
            //Application.Current.Resources.MergedDictionaries[1].MergedDictionaries.Add(new Theme { Type = themeType });
            Application.Current.Resources.MergedDictionaries.Remove(Instance);
            Application.Current.Resources.MergedDictionaries.Add(new Theme { Type = themeType });
        }

        public void GenerateColorFormImage(bool dark = false)
        {
            var colors =  ThemeUtil.Create(@"D:\SwetyCore\文档\MobileFile\f.png", dark);
            
            foreach (PropertyInfo item in typeof(Scheme<Color>).GetProperties())
            {
                //Console.WriteLine(item.Name);

                Add($"{item.Name}Brush", new SolidColorBrush((Color)item.GetValue(colors)));
            }
        }
    }


    public enum ThemeType
    {
        Light, Dark, Other,
        DynamicDark,
        DynamicLight
    }

}
