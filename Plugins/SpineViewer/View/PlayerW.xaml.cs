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
    /// SpinePlayer.xaml 的交互逻辑
    /// </summary>
    public partial class SpinePlayer : UserControl, ICard, IPreviewable
    {
        Window? window => this.Parent as Window;
        private PlayerVM _vm = new PlayerVM();
        public static CardInfo info = new CardInfo(null,"SpineViewer","",typeof(SpinePlayer),CardType.Window);

        public SpinePlayer(Guid guid)
        {
            InitializeComponent();
            GUID = guid;
        }


        private bool _isFirstLoad = true;

        public Guid GUID { get; private set; }

        public int HeightPix => (int)this.Height;

        public int WidthPix => (int)this.Width;

        CardInfo ICard.info => info;

        public void OnDisabled()
        {

            _vm.RemoveSpine();

            _vm.OnExiting(null, null);
        }

        public void OnEnabled()
        {
            if (window==null)
            {
                throw new Exception("找不到父窗口！");
            }
            DataContext = _vm;
            if (_isFirstLoad)
            {
                monoGameControl = new MonoGameContentControl();
                monoGameContainer.Child= monoGameControl;
                monoGameControl.MouseMove += monoGameControl_MouseMove;
                monoGameControl.MouseDown += monoGameControl_MouseDown;
                monoGameControl.MouseUp += monoGameControl_MouseUp;



                var _graphicsDeviceService = _vm.GraphicsDeviceService as MonoGameGraphicsDeviceService;

                _graphicsDeviceService?.StartDirect3D(window);
                _vm?.Initialize();
                _vm?.LoadContent();
                _isFirstLoad = false;



            }
        }

        public void ShowSetting()
        {
            SettingW dlg = new SettingW(_vm);

            dlg.ShowDialog();
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


        #region MonoGameControl Mouse Handler
        bool monoControl_Mouse = false;
        Point monoControl_MousePos;
        private MonoGameContentControl monoGameControl;

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



        public UIElement GetUIElement()
        {
            return this.Content as UIElement;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window?.DragMove();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            window.Topmost = true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            window.Topmost = false;

        }
    }
}
