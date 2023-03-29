using MyWidgets.APP.Common;
using MyWidgets.APP.ViewModel;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using static MyWidgets.APP.Common.PInvoke;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// DockArea.xaml 的交互逻辑
    /// </summary>
    public partial class DockArea : Window
    {

        public DockArea()
        {
            InitializeComponent();

            Left = 0;
            Top = 0;
            Height = SystemParameters.WorkArea.Height;

            App.GetService<SideBarManageService>().Container = sb_container;

            DataContext = App.GetService<SideBarVM>();

            this.Deactivated += DockArea_Deactivated; ;

            this.MouseEnter += DockArea_MouseEnter;
            //App.GetService<SideBarManageService>().ContainerPop = sb_container_pop;

            

        }

        #region 检测全屏

        bool RunningFullScreenApp = false;
        private IntPtr desktopHandle;
        private IntPtr shellHandle;
        int uCallBackMsg;


        public void RegisterAppBar(bool registered)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();

            abd.hWnd = hWnd;
            if (!registered)
            {
                //register   
                uCallBackMsg = APIWrapper.RegisterWindowMessage("APPBARMSG_SC_HELPER");
                abd.uCallbackMessage = uCallBackMsg;
                uint ret = APIWrapper.SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);
            }
            else
            {
                APIWrapper.SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
            }

        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            if (msg == uCallBackMsg)
            {
                switch (wParam.ToInt32())
                {
                    case (int)ABNotify.ABN_FULLSCREENAPP:
                        {
                            IntPtr hWnd = APIWrapper.GetForegroundWindow();
                            //判断当前全屏的应用是否是桌面
                            if (hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle))
                            {
                                RunningFullScreenApp = false;
                                break;
                            }
                            //判断是否全屏
                            if ((int)lParam == 1)
                                this.RunningFullScreenApp = true;
                            else
                                this.RunningFullScreenApp = false;
                            break;
                        }
                    default:
                        break;
                }
            }


            return IntPtr.Zero;

        }
        #endregion

        private void DockArea_Deactivated(object? sender, EventArgs e)
        {
            var storyboard = new Storyboard();
            var widthAnimation = new DoubleAnimation();
            widthAnimation.To = 1;
            Storyboard.SetTarget(widthAnimation, this);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Window.WidthProperty));

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));


            var opacityAnimation = new DoubleAnimation();
            opacityAnimation.To = .1;
            Storyboard.SetTarget(opacityAnimation, this);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Window.OpacityProperty));

            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));

            storyboard.Children.Add(widthAnimation);
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }

        private void DockArea_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //有全屏程序就不触发
            if (RunningFullScreenApp)
            {
                return;
            }
            setForeground();

            var storyboard = new Storyboard();
            var widthAnimation = new DoubleAnimation();
            widthAnimation.To = 448;
            Storyboard.SetTarget(widthAnimation,this);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Window.WidthProperty));

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));

            var opacityAnimation = new DoubleAnimation();
            opacityAnimation.To = 1;
            Storyboard.SetTarget(opacityAnimation, this);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Window.OpacityProperty));

            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));

            storyboard.Children.Add(widthAnimation);
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }


        void setForeground()
        {
            int hForeWnd = GetForegroundWindow();
            int dwForeID = GetWindowThreadProcessId(hForeWnd, 0);
            int dwCurID = GetCurrentThreadId();
            AttachThreadInput(dwCurID, dwForeID, true);
            this.Activate();
            AttachThreadInput(dwCurID, dwForeID, false);
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //为了自动加载已启用的控件
            var vm = App.GetService<SideBarManageVM>();

            WinHide.Hide(this);


            //检测全屏
            RegisterAppBar(false);
            var hs = PresentationSource.FromVisual(this) as HwndSource;
            hs?.AddHook(new HwndSourceHook(WndProc));


        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //da.Close();
            DesktopAppBar.SetAppBar(this, AppBarEdge.None);
            this.Close();
            App.GetService<AppConfigManager>().Save();

            //App.Current.Shutdown();
            Environment.Exit(0);
        }

        private void sb_container_pop_Closed(object sender, EventArgs e)
        {
            (sender as Popup).Child = null;
        }


    }
}
