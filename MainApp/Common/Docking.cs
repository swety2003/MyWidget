using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MainApp.Common
{
    //https://blog.walterlv.com/post/dock-window-into-windows-desktop.html#:~:text=WPF%20%E4%BD%BF%E7%94%A8%20AppBar,%E5%B0%86%E7%AA%97%E5%8F%A3%E5%81%9C%E9%9D%A0%E5%9C%A8%E6%A1%8C%E9%9D%A2%E4%B8%8A%EF%BC%8C%E8%AE%A9%E5%85%B6%E4%BB%96%E7%A8%8B%E5%BA%8F%E4%B8%8D%E5%8D%A0%E7%94%A8%E6%AD%A4%E7%AA%97%E5%8F%A3%E7%9A%84%E7%A9%BA%E9%97%B4%EF%BC%88%E9%99%84%E6%88%91%E5%B0%81%E8%A3%85%E7%9A%84%E9%99%84%E5%8A%A0%E5%B1%9E%E6%80%A7%EF%BC%89%20-%20walterlv

    public enum AppBarEdge
    {
        /// <summary>
        /// 窗口停靠到桌面的左边。
        /// </summary>
        Left = 0,

        /// <summary>
        /// 窗口停靠到桌面的顶部。
        /// </summary>
        Top,

        /// <summary>
        /// 窗口停靠到桌面的右边。
        /// </summary>
        Right,

        /// <summary>
        /// 窗口停靠到桌面的底部。
        /// </summary>
        Bottom,

        /// <summary>
        /// 窗口不停靠到任何方向，而是成为一个普通窗口占用剩余的可用空间（工作区）。
        /// </summary>
        None
    }

    /// <summary>
    /// 提供将窗口停靠到桌面某个方向的能力。
    /// </summary>
    public class DesktopAppBar
    {
        /// <summary>
        /// 标识 Window.AppBar 的附加属性。
        /// </summary>
        public static readonly DependencyProperty AppBarProperty = DependencyProperty.RegisterAttached(
            "AppBar", typeof(AppBarEdge), typeof(DesktopAppBar),
            new PropertyMetadata(AppBarEdge.None, OnAppBarEdgeChanged));

        /// <summary>
        /// 获取 <paramref name="window"/> 当前的停靠边缘。
        /// </summary>
        /// <param name="window">要获取停靠边缘的窗口。</param>
        /// <returns>停靠边缘。</returns>
        public static AppBarEdge GetAppBar(Window window) => (AppBarEdge)window.GetValue(AppBarProperty);

        /// <summary>
        /// 设置 <paramref name="window"/> 的停靠边缘方向。
        /// </summary>
        /// <param name="window">要设置停靠的窗口。</param>
        /// <param name="value">要设置的停靠边缘方向。</param>
        public static void SetAppBar(Window window, AppBarEdge value) => window.SetValue(AppBarProperty, value);

        private static readonly DependencyProperty AppBarProcessorProperty = DependencyProperty.RegisterAttached(
            "AppBarProcessor", typeof(AppBarWindowProcessor), typeof(DesktopAppBar), new PropertyMetadata(null));

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        private static void OnAppBarEdgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var oldValue = (AppBarEdge)e.OldValue;
            var newValue = (AppBarEdge)e.NewValue;
            var oldEnabled = oldValue is AppBarEdge.Left
                                || oldValue is AppBarEdge.Top
                                || oldValue is AppBarEdge.Right
                                || oldValue is AppBarEdge.Bottom;
            var newEnabled = newValue is AppBarEdge.Left
                                || newValue is AppBarEdge.Top
                                || newValue is AppBarEdge.Right
                                || newValue is AppBarEdge.Bottom;
            if (oldEnabled && !newEnabled)
            {
                var processor = (AppBarWindowProcessor)d.GetValue(AppBarProcessorProperty);
                processor.Detach();
            }
            else if (!oldEnabled && newEnabled)
            {
                var processor = new AppBarWindowProcessor((Window)d);
                d.SetValue(AppBarProcessorProperty, processor);
                processor.Attach(newValue);
            }
            else if (oldEnabled && newEnabled)
            {
                var processor = (AppBarWindowProcessor)d.GetValue(AppBarProcessorProperty);
                processor.Update(newValue);
            }
        }

        /// <summary>
        /// 包含对 <see cref="Window"/> 进行操作以便使其成为一个桌面停靠窗口的能力。
        /// </summary>
        private class AppBarWindowProcessor
        {
            /// <summary>
            /// 创建 <see cref="AppBarWindowProcessor"/> 的新实例。
            /// </summary>
            /// <param name="window">需要成为停靠窗口的 <see cref="Window"/> 的实例。</param>
            public AppBarWindowProcessor(Window window)
            {
                _window = window;
                _callbackId = RegisterWindowMessage("AppBarMessage");
                _hwndSourceTask = new TaskCompletionSource<HwndSource>();

                var source = (HwndSource)PresentationSource.FromVisual(window);
                if (source == null)
                {
                    window.SourceInitialized += OnSourceInitialized;
                }
                else
                {
                    _hwndSourceTask.SetResult(source);
                }

                _window.Closed += OnClosed;
            }

            private readonly Window _window;
            private readonly TaskCompletionSource<HwndSource> _hwndSourceTask;
            private readonly int _callbackId;

            private WindowStyle _restoreStyle;
            private Rect _restoreBounds;
            private ResizeMode _restoreResizeMode;
            private bool _restoreTopmost;

            private AppBarEdge Edge { get; set; }

            /// <summary>
            /// 在可以获取到窗口句柄的时候，给窗口句柄设置值。
            /// </summary>
            private void OnSourceInitialized(object sender, EventArgs e)
            {
                _window.SourceInitialized -= OnSourceInitialized;
                var source = (HwndSource)PresentationSource.FromVisual(_window);
                _hwndSourceTask.SetResult(source);
            }

            /// <summary>
            /// 在窗口关闭之后，需要恢复窗口设置过的停靠属性。
            /// </summary>
            private void OnClosed(object sender, EventArgs e)
            {
                _window.Closed -= OnClosed;
                _window.ClearValue(AppBarProperty);
            }

            /// <summary>
            /// 将窗口属性设置为停靠所需的属性。
            /// </summary>
            private void ForceWindowProperties()
            {
                _window.WindowStyle = WindowStyle.None;
                _window.ResizeMode = ResizeMode.NoResize;
                _window.Topmost = true;
            }

            /// <summary>
            /// 备份窗口在成为停靠窗口之前的属性。
            /// </summary>
            private void BackupWindowProperties()
            {
                _restoreStyle = _window.WindowStyle;
                _restoreBounds = _window.RestoreBounds;
                _restoreResizeMode = _window.ResizeMode;
                _restoreTopmost = _window.Topmost;
            }

            /// <summary>
            /// 使一个窗口开始成为桌面停靠窗口，并开始处理窗口停靠消息。
            /// </summary>
            /// <param name="value">停靠方向。</param>
            public async void Attach(AppBarEdge value)
            {
                var hwndSource = await _hwndSourceTask.Task;



                BackupWindowProperties();

                var exstyle = (ulong)GetWindowLongPtr(hwndSource.Handle, GWL_EXSTYLE);
                exstyle |= (ulong)((uint)WS_EX_TOOLWINDOW);
                SetWindowLongPtr(hwndSource.Handle, GWL_EXSTYLE, unchecked((IntPtr)exstyle));

                var data = new APPBARDATA();
                data.cbSize = Marshal.SizeOf(data);
                data.hWnd = hwndSource.Handle;

                data.uCallbackMessage = _callbackId;


                SHAppBarMessage((int)ABMsg.ABM_NEW, ref data);

                hwndSource.AddHook(WndProc);

                Update(value);
            }

            /// <summary>
            /// 更新一个窗口的停靠方向。
            /// </summary>
            /// <param name="value">停靠方向。</param>
            public async void Update(AppBarEdge value)
            {
                var hwndSource = await _hwndSourceTask.Task;

                Edge = value;


                var bounds = TransformToAppBar(hwndSource.Handle, _window.RestoreBounds, value);
                ForceWindowProperties();
                Resize(_window, bounds);
            }

            /// <summary>
            /// 使一个窗口从桌面停靠窗口恢复成普通窗口。
            /// </summary>
            public async void Detach()
            {
                var hwndSource = await _hwndSourceTask.Task;

                var data = new APPBARDATA();
                data.cbSize = Marshal.SizeOf(data);
                data.hWnd = hwndSource.Handle;

                SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref data);

                _window.WindowStyle = _restoreStyle;
                _window.ResizeMode = _restoreResizeMode;
                _window.Topmost = _restoreTopmost;

                Resize(_window, _restoreBounds);
            }

            private IntPtr WndProc(IntPtr hwnd, int msg,
                IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                if (msg == _callbackId)
                {
                    if (wParam.ToInt32() == (int)ABNotify.ABN_POSCHANGED)
                    {
                        var self_bound = new Rect(new Size(_window.Width*DPIHelper.GetScale(),_window.Height));
                        var hwndSource = _hwndSourceTask.Task.Result;
                        var bounds = TransformToAppBar(hwndSource.Handle, self_bound, Edge);
                        Resize(_window, bounds);
                        handled = true;
                    }
                }

                return IntPtr.Zero;
            }

            private static void Resize(Window window, Rect bounds)
            {
                //window.Left = bounds.Left;
                //window.Top = bounds.Top;
                //window.Width = bounds.Width;
                //window.Height = bounds.Height;
            }

            private Rect TransformToAppBar(IntPtr hWnd, Rect area, AppBarEdge edge)
            {
                //area.Width = 48;


                var data = new APPBARDATA();
                data.cbSize = Marshal.SizeOf(data);
                data.hWnd = hWnd;
                data.uEdge = (int)edge;

                if (data.uEdge == (int)AppBarEdge.Left || data.uEdge == (int)AppBarEdge.Right)
                {
                    data.rc.top = 0;
                    data.rc.bottom = (int)SystemParameters.PrimaryScreenHeight;
                    if (data.uEdge == (int)AppBarEdge.Left)
                    {
                        data.rc.left = 0;
                        data.rc.right = (int)Math.Round(area.Width);
                    }
                    else
                    {
                        data.rc.right = (int)SystemParameters.PrimaryScreenWidth;
                        data.rc.left = data.rc.right - (int)Math.Round(area.Width);
                    }
                }
                else
                {
                    data.rc.left = 0;
                    data.rc.right = (int)SystemParameters.PrimaryScreenWidth;
                    if (data.uEdge == (int)AppBarEdge.Top)
                    {
                        data.rc.top = 0;
                        data.rc.bottom = (int)Math.Round(area.Height);
                    }
                    else
                    {
                        data.rc.bottom = (int)SystemParameters.PrimaryScreenHeight;
                        data.rc.top = data.rc.bottom - (int)Math.Round(area.Height);
                    }
                }

                SHAppBarMessage((int)ABMsg.ABM_QUERYPOS, ref data);
                SHAppBarMessage((int)ABMsg.ABM_SETPOS, ref data);

                return new Rect(data.rc.left, data.rc.top,
                    data.rc.right - data.rc.left, data.rc.bottom - data.rc.top);
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct APPBARDATA
            {
                public int cbSize;
                public IntPtr hWnd;
                public int uCallbackMessage;
                public int uEdge;
                public RECT rc;
                public readonly IntPtr lParam;
            }

            private enum ABMsg : int
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

            private enum ABNotify : int
            {
                ABN_STATECHANGE = 0,
                ABN_POSCHANGED,
                ABN_FULLSCREENAPP,
                ABN_WINDOWARRANGE
            }

            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            private static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            private static extern int RegisterWindowMessage(string msg);







            public static IntPtr GetWindowLongPtr(IntPtr hWnd, int index)
    => IntPtr.Size == 4 ? GetWindowLongPtr32(hWnd, index) : GetWindowLongPtr64(hWnd, index);

            [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
            private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int index);

            [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
            private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int index);

            public static IntPtr SetWindowLongPtr(IntPtr hWnd, int index, IntPtr newLong)
    => IntPtr.Size == 4 ? SetWindowLongPtr32(hWnd, index, newLong) : SetWindowLongPtr64(hWnd, index, newLong);

            [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
            private static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int index, IntPtr newLong);

            [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
            private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int index, IntPtr newLong);

            public const int GWL_EXSTYLE = -20;

            public const int WS_EX_TOOLWINDOW = 0x00000080;
        }



        public static class DPIHelper
        {
            public static double GetScale()
            {
                var win = Application.Current.MainWindow;
                var interopWindow = new WindowInteropHelper(win);
                var hwnd = interopWindow.Handle;

                var presentationSource = PresentationSource.FromVisual(win);
                double dpiX = 1.0;
                double dpiY = 1.0;
                if (presentationSource != null)
                {
                    dpiX = presentationSource.CompositionTarget.TransformToDevice.M11;
                    dpiY = presentationSource.CompositionTarget.TransformToDevice.M22;
                }
                return dpiX;
            }
        }
    }


}
