using Microsoft.Extensions.Logging;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyDesktopCards.Common
{
    public static class UIOverrideExt
    {
        static ILogger logger => Logger.LoggerFactory.CreateLogger(nameof(UIOverrideExt));

        const string override_dir = "ui_override";

        public static void TryLoadCustomeStyle<T>(this T self) where T : UserControl
        {
            var name = typeof(T).FullName+".xaml";
            logger.LogDebug($"卡片id:{name}");
            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            var baseDir = Path.Combine(abl,override_dir);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            var xaml_file_path = Path.Combine(baseDir,name);
            if (File.Exists(xaml_file_path))
            {
                try
                {

                    logger.LogInformation($"尝试加载位于:{name} 的样式文件");

                    DependencyObject rootElement;
                    using (FileStream fs = new FileStream(xaml_file_path, FileMode.Open))
                    {
                        rootElement = (DependencyObject)XamlReader.Load(fs);
                    }
                    self.Content = rootElement;


                    logger.LogInformation($"加载成功！");
                }
                catch (Exception ex)
                {
                    logger?.LogError("加载自定义样式出错！", ex);
                }
            }
            else
            {
                logger.LogDebug($"未找到文件:{name}");

            }
        }
    }
}
