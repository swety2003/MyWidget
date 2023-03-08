using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Graphics;
using PluginSDK;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpineViewer.View
{
    /// <summary>
    /// PlayerV.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerV : UserControl,ICard
    {

        private ILogger<PlayerV> _logger => Logger.CreateLogger<PlayerV>();

        public static PlayerV Instance { get; private set; }

        public int HeightPix => 20;

        public int WidthPix =>30;

        public Guid GUID { get; private set; }

        internal static CardInfo info = new CardInfo(null, "SpineViewer", "Spine查看器", typeof(PlayerV));




        private PlayerVM _vm = new PlayerVM();
        public PlayerV(Guid guid)
        {
            InitializeComponent();

            DataContext = _vm;


            GUID = guid;

            Instance = this;
        }

        public void OnEnabled()
        {
            if (_isFirstLoad)
            {
                //_graphicsDeviceService.StartDirect3D(Application.Current.MainWindow);
                _vm?.Initialize();
                _vm?.LoadContent();
                _isFirstLoad = false;
            }
        }

        public void OnDisabled()
        {
            _vm?.OnExiting(this, EventArgs.Empty);
        }

        public void ShowSetting()
        {

            SettingW dlg = new SettingW();
            if (dlg.ShowDialog() == true)
            {
                _vm.AddSpine(dlg.AtlasFile, dlg.SpineFile, dlg.LoaderVersion, dlg.PremultipledAlpha);
            }
        }
    


        //private void mniFileOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    SettingW dlg = new SettingW();
        //    if (dlg.ShowDialog() == true)
        //    {
        //        _vm.AddSpine(dlg.AtlasFile, dlg.SpineFile, dlg.LoaderVersion, dlg.PremultipledAlpha);
        //    }
        //}

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
        private bool _isFirstLoad;

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
    }
}
