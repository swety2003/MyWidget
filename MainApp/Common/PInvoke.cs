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


            IntPtr workerW = FindWindowA("Progman", "Program Manager");
            IntPtr defView = FindWindowExA(workerW, IntPtr.Zero, "SHELLDLL_DefView", "");
            var background = FindWindowExA(defView, IntPtr.Zero, "SysListView32", "FolderView");

            if (background == IntPtr.Zero)
            {
                MessageBox.Show("找不到SysListView32的Handle，程序将以普通模式启动，若想恢复，请尝试重启 explorer.exe后重新打开本软件！");
            };

            return background;
        }
    }
}
