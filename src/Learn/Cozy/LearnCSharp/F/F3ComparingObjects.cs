using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.F
{
    class SomeClass
    {

    }

    class F3ComparingObjects
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Comparing_Reference_Types_for_Equality();
            Comparing_Value_Types_for_Equality();
        }

        public static void Comparing_Reference_Types_for_Equality()
        {
            The_ReferenceEquals_Method();
            The_Virtual_Equals_Method();
            The_Static_Equals_Method();
        }

        public static void The_ReferenceEquals_Method()
        {
            SomeClass class1 = new SomeClass();
            SomeClass class2 = new SomeClass();
            bool b1 = ReferenceEquals(null, null);
            bool b2 = ReferenceEquals(class1, null);
            bool b3 = ReferenceEquals(class1, class2);          // class1 和 class2 指向不同的对象

            Console.WriteLine("{0} {1} {2}", b1, b2, b3);       // 输出true false false
        }

        public static void The_Virtual_Equals_Method()
        {
            Console.WriteLine("暂时跳过 The_Virtual_Equals_Method");
        }

        public static void The_Static_Equals_Method()
        {
            Console.WriteLine("暂时跳过 The_Static_Equals_Method");
        }

        public static void Comparing_Value_Types_for_Equality()
        {
            int i1 = 0;
            int i2 = 0;

            // 装箱
            object obj1 = i1;
            object obj2 = i2;

            bool b1 = i1 == i2;
            bool b2 = ReferenceEquals(obj1, obj2);
            Console.WriteLine("{0} {1}", b1, b2);
        }
    }
}
