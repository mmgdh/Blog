using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class IPHelper
    {
        /// <summary>
        /// 获取本地Ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            var addressList = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
            var ips = addressList.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(address => address.ToString()).ToArray();
            if (ips.Length == 1)
            {
                return ips.First();
            }
            return ips.Where(address => !address.EndsWith(".1")).FirstOrDefault() ?? ips.FirstOrDefault();
        }
    }
}
