using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.H
{
    class H1String
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            string ip = " 11aB3.106.106.";
            ip += "13";
            ip = ip.Trim();
            ip = ip.Replace('a', 'b');
            ip = ip.ToUpper();

            string ip2 = "1";
            StringBuilder build = new StringBuilder(100);
            build.Append(ip);
            build.Append(ip2);
            build.Replace("B", "");

            string ip3 = build.ToString();
            string[] arr = ip3.Split('.');
            foreach (var x in arr)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
}
