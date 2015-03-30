using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    class A2Variable
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            int a = 1;
            Console.WriteLine(a);

            var b = 2;
            Console.WriteLine(b);
        }
    }
}
