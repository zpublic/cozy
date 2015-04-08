using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.F
{
    class F1Operators
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Operator_Shortcuts();
        }

        public static void Operator_Shortcuts()
        {
            The_checked_and_unchecked_Operators();
            The_is_Operator();
            The_as_Operator();
            The_sizeof_Operator();
            The_typeof_Operator();
            Nullable_Types_and_Operators();
            The_Null_Coalescing_Operator();
        }

        public static void The_checked_and_unchecked_Operators()
        {
            byte b = 255; // 自增之后会溢出
            try
            {
                checked
                {
                    ++b;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            Console.WriteLine("b : {0}", b);

            byte ub = 255;
            unchecked
            {
                ++ub;
            }
            Console.WriteLine("ub : {0}", ub);              // 丢失数据 结果为 0
        }

        public static void The_is_Operator()
        {
            string str = "hello";
            object strObject = str;
            if(strObject is string)
            {
                Console.WriteLine("strObject is an string");
            }
            else
            {
                Console.WriteLine("strObject is not an string");
            }
        }

        public static void The_as_Operator()
        {
            object obj1 = "hehe";
            object obj2 = 0;
            string str = obj1 as string;
            string nostr = obj2 as string;

            if(str == null)
            {
                Console.WriteLine("str is null");
            }

            if (nostr == null)
            {
                Console.WriteLine("nostr is null");
            }
        }

        public static void The_sizeof_Operator()
        {
            Console.WriteLine(sizeof(int));
        }

        public static void The_typeof_Operator()
        {
            Console.WriteLine(typeof(string));
        }

        public static void Nullable_Types_and_Operators()
        {
            int? a = null;
            int? b = 0;
            int? c = a + b;     // c = null
            int? d = a + 1;     // d = null
            int? e = b + 1;     // e = 1

            if(a > b)
            {
                Console.WriteLine("a > b");
            }
            else
            {
                Console.WriteLine("a <= b or a == null || b == null ");
            }
        }

        public static void The_Null_Coalescing_Operator()
        {
            int? a = null;
            int? b = 0;
            int c = a ?? 42;
            int d = b ?? 233;
            Console.WriteLine("c : {0} d : {1}", c, d);
        }
    }
}
