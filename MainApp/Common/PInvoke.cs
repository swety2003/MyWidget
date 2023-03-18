using Microsoft.Extensions.Logging;
using PluginSDK;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace MainApp.Common
{
    public static class PInvoke
    {

        [DllImport("user32.dll", EntryPoint = "SetParent")]
        private static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        private static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowExA")]
        private static extern IntPtr FindWindowExA(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "GetClassNameA")]
        private static extern IntPtr GetClassNameA(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "GetParent")]
        private static extern IntPtr GetParent(IntPtr hWnd);


        public static void SetFather(IntPtr form)
        {
            SetParent(form, GetBackground());
        }

        public static void SetFather(IntPtr form, IntPtr father)
        {
            SetParent(form, father);
        }


        private static IntPtr GetBackground()
        {
            var logger = Logger.LoggerFactory.CreateLogger("Find::SysListView32");

            IntPtr background = IntPtr.Zero;
            int count = 0;

            IntPtr dwndparent = FindWindowA("Progman", "Program Manager");
            IntPtr dwndviem = FindWindowExA(dwndparent, IntPtr.Zero, "SHELLDLL_DefView", null);

            background = FindWindowExA(dwndviem, IntPtr.Zero, "SysListView32", "FolderView");


            if (background == IntPtr.Zero)
            {
                dwndparent = FindWindowExA(IntPtr.Zero, IntPtr.Zero, "WorkerW", "");//获得第一个WorkerW类的窗口，
                while (background == IntPtr.Zero)//因为可能会有多个窗口类名为“WorkerW”的窗口存在，所以只能依次遍历
                {
                    count++;
                    dwndviem = FindWindowExA(dwndparent, IntPtr.Zero, "SHELLDLL_DefView", null);
                    dwndparent = FindWindowExA(IntPtr.Zero, dwndparent, "WorkerW", "");

                    background = FindWindowExA(dwndviem, IntPtr.Zero, "SysListView32", "FolderView");

                    logger.LogDebug($"{dwndparent} {dwndviem} {background}");

                    if (count > 100)
                    {
                        break;
                    }
                }

            }

            if (background == IntPtr.Zero)
            {
                MessageBox.Show("找不到SysListView32的Handle，程序将以普通模式启动，若想恢复，请尝试重启 explorer.exe后重新打开本软件！");
            }
            return background;
        }
    }
}
