using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.M
{
    class M1HoodMemoryManagement
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // 主动进行垃圾回收
            GC.Collect();

            // 回收第0代堆内存
            GC.Collect(0);
            GC.Collect(1, GCCollectionMode.Forced);
        }
    }
}
