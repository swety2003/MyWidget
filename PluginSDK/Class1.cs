using Microsoft.Extensions.Logging;
using PluginSDK.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PluginSDK
{


    //public record CardInfo(ImageSource Icon, string Name, string Description, Type MainView,CardType CardType=CardType.UserControl);

    public class CardInfo
    {
        public CardInfo(ImageSource Icon, string Name, string Description, Type MainView, CardType CardType = CardType.UserControl)
        {
            this.Icon = Icon;
            this.Name = Name;
            this.Description = Description;
            this.MainView = MainView;
            this.CardType = CardType;

        }

        public ImageSource Icon { get; }
        public string Name { get; }
        public string Description { get; }
        public Type MainView { get; }
        public CardType CardType { get; }
    }


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

        // 不能使用 CardInfo ICard.CI => info; ，否则无法绑定成功
        public CardInfo CI { get; }

    }

    

    public interface ISideBarItem: IViewBase
    {

        Popup Popup { get; }
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
            return LoggerFactory?.CreateLogger<T>() ?? throw new Exception("Logger.LoggerFactory 未初始化！");
        }

    }


    public static class UCExt
    {

        public static string GetPluginConfigFilePath(this IViewBase self)
        {
            string ret;

            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            try
            {
                ret = Path.Combine(abl, "Configs", $"{self.GUID}.json");
            }
            catch (Exception ex)
            {
                ret = Path.Combine(abl, "Configs", $"config.json");
            }

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

    public static class ISideBarItemExt
    {
        public static void Show(this ISideBarItem card,UIElement view)
        {
            card.Popup.Child = view;
            card.Popup.IsOpen = true;
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
