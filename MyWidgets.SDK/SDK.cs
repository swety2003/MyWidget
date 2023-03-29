using Microsoft.Extensions.Logging;
using MyWidgets.SDK.Controls;
using MyWidgets.SDK.Core.Card;
using MyWidgets.SDK.Core.SideBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace MyWidgets.SDK
{


    public interface IPlugin
    {
        public string name { get; }
        public Version version { get; }
        public string url { get; }
        public string author { get; }

        public IEnumerable<object> GetAllTypeInfo();

    }

    public interface IViewBase
    {
        public void OnEnabled();
        public void OnDisabled();

        public void ShowSetting();
    }




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






}
