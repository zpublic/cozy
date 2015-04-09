using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class E7Tuple
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Tuples();
        }

        public static void Tuples()
        {
            // 创建元组
            Tuple<int, int> tp1 = Tuple.Create(1, 2);
            Console.Write("{0} {1}\n", tp1.Item1, tp1.Item2);

            // 创建大于8个的元组
            var tp2 = Tuple.Create<int, int, int, int, int, int, int, Tuple<int, int>>(1, 2, 3, 4, 5, 6, 7,
                Tuple.Create<int, int>(8, 9));
            Console.Write("{0} {1} {2}", tp2.Item7, tp2.Rest.Item1.Item1, tp2.Rest.Item1.Item2);
        }
    }
}
