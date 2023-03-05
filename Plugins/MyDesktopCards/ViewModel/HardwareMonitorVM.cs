using CommunityToolkit.Mvvm.ComponentModel;
using CZGL.SystemInfo;
using MyDesktopCards.Common;
using System;
using System.Windows.Threading;

namespace MyDesktopCards.ViewModel
{
    internal partial class HardwareMonitorVM : SimpleVM
    {



        private CPUTime v1;
        private NetworkInfo network;
        private Rate oldRate;


        public HardwareMonitorVM()
        {
            v1 = CPUHelper.GetCPUTime();
            network = NetworkInfo.TryGetRealNetworkInfo() ?? throw new Exception();
            oldRate = network.GetIpv4Speed();

            OnActiveChanged += HardwareMonitorVM_OnActiveChanged;

        }

        private void HardwareMonitorVM_OnActiveChanged(object? sender, bool e)
        {
            if (e)
            {

                _Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                _Timer.Tick += _Timer_Tick;
                _Timer.Start();
            }
            else
            {

                _Timer.Tick -= _Timer_Tick;

                _Timer.Stop();
            }
        }

        public record NetItem(string size, string type);



        [ObservableProperty]
        NetItem upload;

        [ObservableProperty]
        NetItem download;

        [ObservableProperty]
        int cpuLoad;

        [ObservableProperty]
        int ramLoad;


        private void _Timer_Tick(object? sender, EventArgs e)
        {
            var v2 = CPUHelper.GetCPUTime();
            var value = CPUHelper.CalculateCPULoad(v1, v2);
            v1 = v2;

            var memory = MemoryHelper.GetMemoryValue();
            var newRate = network.GetIpv4Speed();
            var speed = NetworkInfo.GetSpeed(oldRate, newRate);
            oldRate = newRate;

            CpuLoad = (int)(value * 100);
            RamLoad = (int)memory.UsedPercentage;
            Upload = new NetItem(speed.Sent.Size.ToString(), speed.Sent.SizeType.ToString());
            Download = new NetItem(speed.Received.Size.ToString(), speed.Received.SizeType.ToString());

        }

    }
}
