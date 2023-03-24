using MaterialColorUtilities.Schemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace MaterialDesign3.Styles.Colors
{
    [Localizability(LocalizationCategory.Ignore)]
    [Ambient]
    [UsableDuringInitialization(true)]
    public class ColorSystem : ResourceDictionary
    {
        public static ColorSystem? Instance;
        public ColorSystem()
        {
            Instance = this;
        }

        public ColorSystem(ThemeType type):base ()
        {
            Type = type;
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
                        this.Source = new Uri($"pack://application:,,,/MaterialDesign3;component/Styles/Colors/LightColor.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.Dark:
                        //深色主题
                        this.Source = new Uri($"pack://application:,,,/MaterialDesign3;component/Styles/Colors/DarkColor.xaml", UriKind.Absolute);
                        break;
                    case ThemeType.DynamicDark:

                        GenerateDynamicColor(true);
                        break;
                    case ThemeType.DynamicLight:

                        GenerateDynamicColor();
                        break;
                    default:
                        this.Source = new Uri($"pack://application:,,,/MaterialDesign3;component/Styles/Colors/DarkColor.xaml", UriKind.Absolute);

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
            Application.Current.Resources.MergedDictionaries.Add(new ColorSystem { Type = themeType });
        }

        public void GenerateDynamicColor(bool dark = false)
        {
            var colors = ThemeBuilder.Create( dark);

            foreach (PropertyInfo item in typeof(Scheme<Color>).GetProperties())
            {
                //Console.WriteLine(item.Name);

                Add($"{item.Name}",item.GetValue(colors));
            }
        }
    }


    public enum ThemeType
    {
        Light, Dark,
        DynamicDark,
        DynamicLight
    }
}
