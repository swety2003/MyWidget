
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using PluginSDK;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static DefaultWidgets.Utils.Weather.DataType;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : UserControl, ICard
    {
        public int HeightPix => 4;

        public int WidthPix => 4;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "天气", "当前天气", typeof(Weather));


        void ICard.OnEnabled()
        {
        }

        void ICard.OnDisabled()
        {
        }

        WeatherVM vm = new WeatherVM();
        public Weather(Guid g)
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = vm;
            RefreshAsync();

        }


        private async Task RefreshAsync()
        {

            try
            {
                Utils.Weather.API api = new Utils.Weather.API();
                var resp = await api.GetCurrent();

                vm.Weather = JsonConvert.DeserializeObject<WeatherCurrent.Root>(resp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshAsync();

        }
    }

    partial class WeatherVM : ObservableObject
    {
        [ObservableProperty]
        private WeatherCurrent.Root _weather;




    }
}
