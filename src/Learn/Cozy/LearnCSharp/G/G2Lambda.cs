using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.G
{
    class G2Lambda
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Parameters();
            Multiple_Code_Lines();
            Closures();
            Closures_Do();
        }

        public static void Parameters()
        {
            // 单一参数
            Func<int, string> toStr = i =>
            {
                return i.ToString();
            };
            Console.WriteLine(toStr(42));

            // 多个参数
            Func<int, int, int> Add = (a, b) =>
                {
                    return a + b;
                };
            Console.WriteLine(Add(1, 2));

            // 带参数类型的多个参数
            Func<double, double, double> AddDouble = (double a, double b) =>
            {
                return a + b;
            };
            Console.WriteLine(AddDouble(1.2, 2.3));
        }

        public static void Multiple_Code_Lines()
        {
            // 单行lambda
            Func<int, int> Power2 = n => n * n;
            Console.WriteLine(Power2(2));
        }


        public static Func<int, int> DoSomething = null;
        public static void Closures()
        {
            // 闭包
            int someVar = 5;
            DoSomething = n => n + someVar;
            Console.WriteLine(DoSomething(3));
            someVar = 3;
            Console.WriteLine(DoSomething(3));
        }

        public static void Closures_Do()
        {
            Console.WriteLine(DoSomething(3));
        }
    }
}
