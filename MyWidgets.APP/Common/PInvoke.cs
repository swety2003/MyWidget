using Microsoft.Extensions.Logging;
using MyWidgets.SDK;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace MyWidgets.APP.Common
{
    public static class PInvoke
    {

        #region Win32导入-侧栏

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(int hwnd, int lpdwProcessId);
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        public static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);


        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体


        public class APIWrapper
        {
            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern int RegisterWindowMessage(string msg);

            //取得Shell窗口句柄函数 
            [DllImport("user32.dll")]
            public static extern IntPtr GetShellWindow();
            //取得桌面窗口句柄函数 
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            //取得前台窗口句柄函数 
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }
        public enum ABMsg : int
        {
            ABM_NEW = 0,
            ABM_REMOVE,
            ABM_QUERYPOS,
            ABM_SETPOS,
            ABM_GETSTATE,
            ABM_GETTASKBARPOS,
            ABM_ACTIVATE,
            ABM_GETAUTOHIDEBAR,
            ABM_SETAUTOHIDEBAR,
            ABM_WINDOWPOSCHANGED,
            ABM_SETSTATE
        }
        public enum ABNotify : int
        {
            ABN_STATECHANGE = 0,
            ABN_POSCHANGED,
            ABN_FULLSCREENAPP,
            ABN_WINDOWARRANGE
        }
        public enum ABEdge : int
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }


        #endregion

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
