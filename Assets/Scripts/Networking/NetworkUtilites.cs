using System;
using System.Net.NetworkInformation;

namespace Networking
{
    static class NetworkUtilites
    {
        public static string GetLocalIPAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                            && ip.Address.ToString().StartsWith("192"))
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
