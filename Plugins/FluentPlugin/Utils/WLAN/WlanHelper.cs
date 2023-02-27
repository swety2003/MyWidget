// Created by Leestar54
// E-Mail:178078114@qq.com
using NativeWifi;
using System;
using System.Diagnostics;
using System.Text;

namespace Wifi连接器
{
    /// <summary>
    /// Wlan小助手
    /// </summary>
    public static class WlanHelper
    {
        //是否联网dll
        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);


        /// <summary>
        /// 设置自动连接，有些系统通过xml设置无效，只能用cmd了
        /// </summary>
        /// <param name="profile"></param>
        public static void SetAutoConnect(string profile)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //输出信息重定向
                p.StartInfo.CreateNoWindow = true;
                //可能接受来自调用程序的输入信息  
                p.StartInfo.RedirectStandardInput = true;
                //由调用程序获取输出信息  
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();                    //启动线程
                p.StandardInput.WriteLine("netsh wlan set profileparameter name=\"" + profile + "\" connectionmode=auto");
                p.StandardInput.WriteLine("exit");
                p.WaitForExit();            //等待进程结束
            }
        }

        /// <summary>
        /// 一键连接wifi网络
        /// </summary>
        /// <param name="network">加密网络</param>
        /// <param name="password">密码</param>
        public static void ConnetWifi(WlanClient.WlanInterface wlanIface, Wlan.WlanAvailableNetwork network, string password)
        {
            string profile = GetStringForSSID(network.dot11Ssid);
            if (password != null)
            {
                string hex = StringToHex(profile);
                string authentication = GetAuthentication(network.dot11DefaultAuthAlgorithm);
                string authEncryption = GetEncryption(network.dot11DefaultCipherAlgorithm);
                string keytype = GetKeyType(authEncryption);
                string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>auto</connectionMode><MSM><security><authEncryption><authentication>{2}</authentication><encryption>{3}</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>{4}</keyType><protected>false</protected><keyMaterial>{5}</keyMaterial></sharedKey></security></MSM></WLANProfile>", profile, hex, authentication, authEncryption, keytype, password);
                wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profile);
            }
            else
            {
                //有profile的加密连接，直接连
                if (network.securityEnabled)
                {
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profile);
                }
                else
                {
                    string hex = StringToHex(profile);
                    string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>auto</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profile, hex);
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profile);
                }
            }
        }

        /// <summary>
        /// 一键连接wifi网络
        /// </summary>
        /// <param name="network">公开网络</param>
        public static void ConnetWifi(WlanClient.WlanInterface wlanIface, Wlan.WlanAvailableNetwork network)
        {
            ConnetWifi(wlanIface, network, null);
        }

        /// <summary>
        /// 是否有身份配置，有就直接连接了，不用输入什么密码了
        /// </summary>
        /// <param name="wlanIface"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static bool HasProfile(WlanClient.WlanInterface wlanIface, string profile)
        {
            Wlan.WlanProfileInfo[] p = wlanIface.GetProfiles();
            foreach (Wlan.WlanProfileInfo item in p)
            {
                if (item.profileName == profile)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取wifi信息提示
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        public static string GetWifiToolTip(Wlan.WlanAvailableNetwork network)
        {
            string result = string.Format("SSID：{0}\n信号强度：{1}\n安全类型：{2}", GetStringForSSID(network.dot11Ssid), network.wlanSignalQuality, GetAuthentication(network.dot11DefaultAuthAlgorithm));
            return result;
        }

        /// <summary>
        /// 是否连接网络
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedInternet()
        {
            System.Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// SSID名称转换
        /// </summary>
        /// <param name="ssid">ssid的byte格式</param>
        /// <returns></returns>
        public static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }
        /// <summary>  
        /// 字符串转Hex  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string StringToHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)  
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString().ToUpper());
        }

        /// <summary>
        /// 获取认证方式名称
        /// </summary>
        /// <returns></returns>
        public static string GetAuthentication(NativeWifi.Wlan.Dot11AuthAlgorithm daa)
        {
            string r = string.Empty;
            switch (daa)
            {
                case Wlan.Dot11AuthAlgorithm.IEEE80211_Open:
                    r = "open";
                    break;
                case Wlan.Dot11AuthAlgorithm.IEEE80211_SharedKey:
                    r = "shared";
                    break;
                case Wlan.Dot11AuthAlgorithm.WPA:
                    r = "WPA";
                    break;
                case Wlan.Dot11AuthAlgorithm.WPA_PSK:
                    r = "WPAPSK";
                    break;
                case Wlan.Dot11AuthAlgorithm.WPA_None:
                    r = "WPA";
                    break;
                case Wlan.Dot11AuthAlgorithm.RSNA:
                    r = "WPA2";
                    break;
                case Wlan.Dot11AuthAlgorithm.RSNA_PSK:
                    r = "WPA2PSK";
                    break;
                case Wlan.Dot11AuthAlgorithm.IHV_Start:
                    break;
                case Wlan.Dot11AuthAlgorithm.IHV_End:
                    break;
                default:
                    break;
            }
            return r;
        }

        /// <summary>
        /// 获取加密方式名称
        /// </summary>
        /// <returns></returns>
        public static string GetEncryption(NativeWifi.Wlan.Dot11CipherAlgorithm dca)
        {
            string r = string.Empty;
            switch (dca)
            {
                case Wlan.Dot11CipherAlgorithm.None:
                    r = "none";
                    break;
                case Wlan.Dot11CipherAlgorithm.WEP40:
                    r = "WEP";
                    break;
                case Wlan.Dot11CipherAlgorithm.TKIP:
                    r = "TKIP";
                    break;
                case Wlan.Dot11CipherAlgorithm.CCMP:
                    r = "AES";
                    break;
                case Wlan.Dot11CipherAlgorithm.WEP104:
                    r = "WEP";
                    break;
                case Wlan.Dot11CipherAlgorithm.WEP:
                    r = "WEP";
                    break;
                case Wlan.Dot11CipherAlgorithm.IHV_Start:
                    break;
                case Wlan.Dot11CipherAlgorithm.IHV_End:
                    break;
                default:
                    break;
            }
            return r;
        }

        /// <summary>
        /// 获取密匙类型
        /// </summary>
        /// <returns></returns>
        public static string GetKeyType(string encryption)
        {
            switch (encryption)
            {
                case "WEP":
                    return "networkKey";
                    break;
                default:
                    return "passPhrase";
                    break;
            }
        }
    }

}
