using PluginSDK.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace PluginSDK
{


    public record CardInfo(ImageSource Icon, string Name, string Description, Type MainView);

    public interface IPlugin
    {
        public string name { get; }
        public Version version { get; }
        public string url { get; }
        public string author { get; }

        public List<CardInfo> GetAllCards();
    }

    public interface ICard
    {
        public int HeightPix { get; }
        public int WidthPix { get; }

        public Guid GUID { get; }



        public void OnEnabled();
        public void OnDisabled();


        //public void OnAppClosed();

    }

    public static class UCExt
    {
        public static string GetPluginConfigFilePath(this UserControl self)
        {
            var @card = self as ICard;

            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);

            var ret = Path.Combine(abl, "Configs", $"{card.GUID}.json");

            if (!Directory.Exists(Path.GetDirectoryName(ret)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ret));
            }
            return ret;
        }

        public static void ResizeCard(this UserControl self, int h, int w)
        {

            var t = self?.Parent as MyThumb;

            if (t == null)
            {
                return;
            }


            t.HeightPix = h;
            t.WidthPix = w;
        }
    }


}
