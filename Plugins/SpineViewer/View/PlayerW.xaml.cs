using Microsoft.Xna.Framework.Graphics;
using PluginSDK;
using SpineViewer.MonoGameControls;
using SpineViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpineViewer.View
{
    /// <summary>
    /// PlayerW.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerW : Window,IWindow
    {
        public static PlayerW Instance { get; private set; }

        private PlayerVM _vm = new PlayerVM();
        public static WindowInfo info = new WindowInfo("SpineViewerW","",typeof(PlayerW));
        public PlayerW()
        {
            InitializeComponent();
            //GUID= guid;
            if (Instance==null)
            {

                Instance = this;
            }
            else
            {
                throw new Exception("");
            }
            ContentRendered += PlayerW_ContentRendered;
            DataContext = _vm;
        }


        private bool _isFirstLoad = true;
        private void PlayerW_ContentRendered(object? sender, EventArgs e)
        {
            if (_isFirstLoad)
            {
                var _graphicsDeviceService = _vm.GraphicsDeviceService as MonoGameGraphicsDeviceService;

                _graphicsDeviceService?.StartDirect3D(this);
                _vm?.Initialize();
                _vm?.LoadContent();
                _isFirstLoad = false;
            }
        }

        public Guid GUID { get; private set; }

        public void OnDisabled()
        {

            _vm?.OnExiting(this, EventArgs.Empty);
        }

        public void OnEnabled()
        {
            //if (_isFirstLoad)
            //{
            //    //_graphicsDeviceService.StartDirect3D(Application.Current.MainWindow);
            //    _vm?.Initialize();
            //    _vm?.LoadContent();
            //    _isFirstLoad = false;
            //}
        }

        public void ShowSetting()
        {
            SettingW dlg = new SettingW(_vm);
            if (dlg.ShowDialog() == true)
            {
                _vm.AddSpine(dlg.AtlasFile, dlg.SpineFile, dlg.LoaderVersion, dlg.PremultipledAlpha);
            }
        }

        private void btUnloadSpine_Click(object sender, RoutedEventArgs e)
        {
            _vm.RemoveSpine();
        }

        private void btApplySpine_Click(object sender, RoutedEventArgs e)
        {
            _vm.ApplySpine();
        }

        private void btPlay_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsPausing = !_vm.IsPausing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }


        #region MonoGameControl Mouse Handler
        bool monoControl_Mouse = false;
        Point monoControl_MousePos;

        private void monoGameControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            monoControl_Mouse = monoGameControl.CaptureMouse();
            monoControl_MousePos = e.GetPosition(monoGameControl);
        }

        private void monoGameControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (monoControl_Mouse)
            {
                Point mp = e.GetPosition(monoGameControl);
                _vm.Camera.Move((float)(monoControl_MousePos.X - mp.X), (float)(monoControl_MousePos.Y - mp.Y));
                monoControl_MousePos = mp;
            }
        }

        private void monoGameControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (monoControl_Mouse)
            {
                monoGameControl.ReleaseMouseCapture();
                monoControl_Mouse = false;
            }
        }
        #endregion

        private void mniHelpSaveLayout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btSetting_Click(object sender, RoutedEventArgs e)
        {
            this.ShowSetting();


        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            _vm.RemoveSpine();

            _vm.OnExiting(null,null);
            this.Close();
            
        }
    }
}
