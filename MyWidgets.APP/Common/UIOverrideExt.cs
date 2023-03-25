using Microsoft.Extensions.Logging;
using MyWidgets.SDK;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyWidgets.APP.Common
{
    public static class UIOverrideExt
    {
        static ILogger logger = Logger.LoggerFactory.CreateLogger(nameof(UIOverrideExt));

        const string override_dir = "custome";
        public static void TryLoadCustomeStyle<T>(this UserControl self)
        {
            var name = typeof(T).FullName;
            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            var baseDir = Path.Combine(abl, override_dir);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            var xaml_file_path = Path.Combine(baseDir, name);
            if (File.Exists(xaml_file_path))
            {
                try
                {

                    DependencyObject rootElement;
                    using (FileStream fs = new FileStream(xaml_file_path, FileMode.Open))
                    {
                        rootElement = (DependencyObject)XamlReader.Load(fs);
                    }
                    self.Content = rootElement;
                }
                catch (Exception ex)
                {
                    logger?.LogError("加载自定义样式出错！", ex);
                }
            }
        }
    }
}
