using System.Runtime.InteropServices;

namespace DefaultWidgets.Utils
{
    //https://blog.csdn.net/KYuruyan/article/details/107094941
    internal class SysBatteryInfo
    {
        public enum ACLineStatus_ : byte
        {
            Offline = 0,
            Online = 1,  // 
            UnknowStatus = 255  // 未知
        }

        public enum BatteryFlag_ : byte
        {  // 虽然是枚举，但可以有多个值
            Middle = 0,  // 电池未充电并且电池容量介于高电量和低电量之间
            Low = 2,  // 电池电量不足33％
            Critical = 4,  // 电池电量不足百分之五
            Charging = 8,  // 	充电中
            NoSystemBattery = 128,  // 无系统电池
            UnknowStatus = 255  // 无法读取电池标志信息
        }

        public enum SystemStatusFlag_ : byte
        {
            Off = 0,  //  节电功能已关闭
            On = 1  //  节电功能已打开，节省电池。尽可能节约能源
        }

        public struct SystemPowerStatus
        {  // 顺序不可更改
            public ACLineStatus_ ACLineStatus;  // 交流电源状态
            public BatteryFlag_ BatteryFlag;  // 电池充电状态
            public byte BatteryLifePercent;  // 剩余电量的百分比。该成员的值可以在0到100的范围内，如果状态未知，则可以是255
            public SystemStatusFlag_ SystemStatusFlag;  //  省电模式
            public int BatteryLifeTime;  //  剩余电池寿命的秒数。如果未知剩余秒数或设备连接到交流电源，则为–1
            public int BatteryFullLifeTime;  // 充满电时的电池寿命秒数。如果未知电池的完整寿命或设备连接到交流电源，则为–1。
        }


        [DllImport("Kernel32.dll")]
        public static extern bool GetSystemPowerStatus(ref SystemPowerStatus systemPowerStatus);

        public SystemPowerStatus Get()
        {
            SystemPowerStatus status = new SystemPowerStatus();
            if (GetSystemPowerStatus(ref status))
            {  // 如果成功调用
                return status;
            }
            else
            {
                return new SystemPowerStatus();
            }
        }

    }
}
