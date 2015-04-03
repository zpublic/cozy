using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.D
{
    class D4GenericStruct
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            NullAbleStruct();
        }

        // 判断Nullable是否为空
        public static string PrintNullAble(Nullable<int> x)
        {
            if (x.HasValue)
            {
                return x.Value.ToString();
            }
            else
            {
                return "No Value";
            }
        }

        public static void NullAbleStruct()
        {
            Nullable<int> x = null;
            Console.WriteLine(PrintNullAble(x));        // 输出 No Value
            x = 42;
            Console.WriteLine(PrintNullAble(x));        // 输出 42

            // 定义int?类型
            int? x1 = null;
            Console.WriteLine(PrintNullAble(x1));       // 输出 No Value
            x1 = 42;
            Console.WriteLine(PrintNullAble(x1));       // 输出 42

            int? x2 = null;
            int? x3 = 1;
            int? x4 = x2 + x3;
            Console.WriteLine(PrintNullAble(x4));       // 输出 No Value

            int x5 = x2 ?? 233;
            Console.WriteLine(PrintNullAble(x5));       // 输出233
            int x6 = x3 ?? 233;
            Console.WriteLine(PrintNullAble(x6));       // 输出1
        }
    }
}
