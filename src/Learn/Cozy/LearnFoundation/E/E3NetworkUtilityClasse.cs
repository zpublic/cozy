using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Cozy.LearnFoundation.E
{
    class E3NetworkUtilityClasse
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            URIS();
            IP_Address_and_DNS_Names();
        }

        public static void URIS()
        {
            const string url_name = @"https://www.baidu.com/s?wd=cozy";

            // 创建Uri实例并获取属性
            Uri page = new Uri(url_name);
            Console.WriteLine(url_name);
            Print_URI(page);

            // 使用UriBuilder
            UriBuilder builder = new UriBuilder("http", "www.baidu.com", 80, "index.html");
            Uri buildUri = builder.Uri;
            Print_URI(buildUri);
        }

        public static void Print_URI(Uri page)
        {
            Console.WriteLine(page.Query);
            Console.WriteLine(page.AbsolutePath);
            Console.WriteLine(page.Scheme);
            Console.WriteLine(page.Port);
            Console.WriteLine(page.Host);
            Console.WriteLine(page.IsDefaultPort);
        }

        public static void IP_Address_and_DNS_Names()
        {
            // IPAddress和string互相转化
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            byte[] address = ipAddress.GetAddressBytes();
            string ipStr = ipAddress.ToString();

            Console.WriteLine(ipStr);

            // 特殊的地址 
            string loopback = IPAddress.Loopback.ToString();        // 127.0.0.1
            string broadcast = IPAddress.Broadcast.ToString();      // 255.255.255.255

            Console.WriteLine(loopback);
            Console.WriteLine(broadcast);

            try
            {
                // 与服务器通讯获取详细信息
                IPHostEntry host = Dns.GetHostEntry("www.baidu.com");
                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine("IpAddress : {0}", ip.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
            // 输入的地址无效或者网络断开会产生异常
        }
    }
}
