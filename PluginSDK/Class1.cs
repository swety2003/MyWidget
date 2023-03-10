using Microsoft.Extensions.Logging;
using PluginSDK.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PluginSDK
{


    public record CardInfo(ImageSource Icon, string Name, string Description, Type MainView,CardType CardType=CardType.UserControl);

    public interface IPlugin
    {
        public string name { get; }
        public Version version { get; }
        public string url { get; }
        public string author { get; }

        public List<CardInfo> GetAllCards();

        public List<SideBarItemInfo> GetAllSBItems();
    }

    public interface IViewBase
    {

        public Guid GUID { get; }

        public void OnEnabled();
        public void OnDisabled();

        public void ShowSetting();
    }

    public enum CardType
    {
        Window,UserControl
    }

    public interface ICard: IViewBase,IPreviewable
    {

        public int HeightPix { get; }
        public int WidthPix { get; }

        public CardInfo info { get; }

    }

    

    public interface ISideBarItem: IViewBase
    {


    }

    public record SideBarItemInfo(string Name, string Description, Type MainView);

    public static class Logger
    {

        private static ILoggerFactory? _loggerFactory;

        public static ILoggerFactory? LoggerFactory
        {
            get { return _loggerFactory; }
            set 
            {
                if (_loggerFactory == null)
                {
                    _loggerFactory = value;
                }
            }
        }

        public static ILogger<T> CreateLogger<T>()
        {
            return LoggerFactory?.CreateLogger<T>() ?? throw new Exception("Logger.LoggerFactory Œ¥≥ı ºªØ£°");
        }

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

            var t = self?.Parent as CardControl;

            if (t == null)
            {
                return;
            }


            t.HeightPix = h;
            t.WidthPix = w;
        }
    }

    public static class ICardExt
    {
        public static CardControl GetCardControl(this ICard card)
        {

            var c = (card as UserControl).Parent as CardControl;
            return c;
        }

        public static CardWindow GetCardWindow(this ICard card)
        {

            var win = (card as UserControl).Parent as CardWindow;
            return win;
        }
    }

    public interface ICanOverrideUI
    {
        void OverrideUI(string xaml_file_path);
    }


    public interface IPreviewable
    {
        public UIElement GetUIElement();

        
    }
}
