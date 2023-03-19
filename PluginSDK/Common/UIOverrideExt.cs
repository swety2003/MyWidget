using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

namespace PluginSDK.Common
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

                    UIElement rootElement;


                    var xaml_text = File.ReadAllText(xaml_file_path);

                    rootElement = (UIElement)XamlReader.Parse(xaml_text);


                    var width = OverrideUIInfo.GetWidth(rootElement);
                    var height = OverrideUIInfo.GetHeight(rootElement);

                    self.ResizeCard(height, width);


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

    public class OverrideUIInfo
    {
        // Register an attached dependency property with the specified
        // property name, property type, owner type, and property metadata.
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached(
          "Height",
          typeof(int),
          typeof(OverrideUIInfo),
          new FrameworkPropertyMetadata(defaultValue: 4,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static int GetHeight(UIElement target) =>
            (int)target.GetValue(HeightProperty);

        // Declare a set accessor method.
        public static void SetHeight(UIElement target, int value) =>
            target.SetValue(HeightProperty, value);


        public static readonly DependencyProperty Width =
            DependencyProperty.RegisterAttached(
          "Width",
          typeof(int),
          typeof(OverrideUIInfo),
          new FrameworkPropertyMetadata(defaultValue: 4,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static int GetWidth(UIElement target) =>
            (int)target.GetValue(Width);

        // Declare a set accessor method.
        public static void SetWidth(UIElement target, int value) =>
            target.SetValue(Width, value);
    }


}
