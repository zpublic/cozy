using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    class A7Console
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Console_IO();

        }

        public static void Console_IO()
        {
            // 输入一行
            string str_line = Console.ReadLine();
            Console.WriteLine(str_line);                                // 输出输入的一行
            
            // 输出
            int i = 1;
            int j = 2;
            float f = 1.0F;
            Console.Write(i);                                           // 输出 1 
            Console.WriteLine(j);                                       // 输出 2 并换行
            Console.WriteLine(f);                                       // 输出 1

            // 格式化输出
            Console.WriteLine("{0}", f);                                // 输出 1
            Console.WriteLine("{0,0:F2}", f);                           // 输出 1.00
            Console.WriteLine("-{0,8:F2}-", f);                         // 输出 -    1.00-
            Console.WriteLine("-{0,-8:F2}-", f);                        // 输出 -1.00    -
            Console.WriteLine("value of i : {0}", i);                   // 输出 value of i : 1
            Console.WriteLine("value of j : {0}", j);                   // 输出 value of j : 2
            Console.WriteLine("value of i and j : {0} {1}", i, j);      // 输出 value of i and j : 1 2
            Console.WriteLine("value of i and j : {0,4} {1}", i, j);    // 输出 value of i and j :    12
            Console.WriteLine("value of i and j : {0,-4} {1}", i, j);   // 输出 value of i and j : 1   2
            Console.WriteLine("{0:#.000}", 1.0f);                       // 输出 1.000
            Console.WriteLine("{0:#.000}", 0.0f);                       // 输出 .000

        }
    }
}
