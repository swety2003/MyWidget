using Microsoft.Extensions.Logging;
using MyWidgets.SDK.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MyWidgets.SDK
{


    public record CardInfo(ImageSource Icon, string Name, string Description, Type MainView, CardType CardType = CardType.UserControl);



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
        Window, UserControl
    }

    public interface ICard : IViewBase, IPreviewable
    {

        public int HeightPix { get; }
        public int WidthPix { get; }

        // ����ʹ�� CardInfo ICard.Info => info; �������޷��󶨳ɹ�
        public CardInfo Info { get; }

    }

    public interface ISideBarItem : IViewBase
    {
        public SideBarItemInfo Info { get; }
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
            return LoggerFactory?.CreateLogger<T>() ?? throw new Exception("Logger.LoggerFactory δ��ʼ����");
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

            var t = (self as ICard)?.GetCardControl();

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
        /// <summary>
        /// ���Ҹ��ؼ�
        /// </summary>
        /// <typeparam name="T">���ؼ�������</typeparam>
        /// <param name="obj">Ҫ�ҵ���obj�ĸ��ؼ�</param>
        /// <param name="name">���ҵĸ��ؼ���Name����</param>
        /// <returns>Ŀ�길�ؼ�</returns>
        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                // ����һ�����ؼ���û���ҵ�ָ�����ֵĿؼ�����������һ����
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }


        public static CardControl GetCardControl(this ICard card)
        {
            var c = GetParentObject<CardControl>(card as DependencyObject);
            return c;
        }

        public static CardWindow GetCardWindow(this ICard card)
        {

            var win = GetParentObject<CardWindow>(card as DependencyObject);
            return win;
        }
    }




    public delegate void ShowCard(UIElement element);

    public static class ISideBarItemExt
    {
        public static ShowCard? ShowCard { get; private set; }

        public static void SetMethod(ShowCard action)
        {
            if (ShowCard==null)
            {
                ShowCard = action;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static void Show(this ISideBarItem card, UIElement view)
        {
            ShowCard?.Invoke(view);
        }
    }



    public interface IPreviewable
    {
        public UIElement GetUIElement();


    }
}
