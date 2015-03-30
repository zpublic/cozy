using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.C
{
    class Class1
    {
        public virtual string S1()
        {
            return "class1";
        }
    }

    interface Interface1
    {
        string S2();
    }

    class Class2 : Class1
    {
        public override string S1()
        {
            return "class2";
        }
    }

    class Class3 : Interface1
    {
        public string S2()
        {
            return "class3";
        }
    }

    class Class4 : Class1, Interface1
    {
        public override string S1()
        {
            return "class4_1";
        }

        public string S2()
        {
            return "class4_2";
        }
    }

    class C1InheritanceType
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // 实现继承
            Class2 c2 = new Class2();
            Console.WriteLine(c2.S1());

            // 接口继承
            Class3 c3 = new Class3();
            Console.WriteLine(c3.S2());

            // 多重继承
            Class4 c4 = new Class4();
            Console.WriteLine(c4.S1());
            Console.WriteLine(c4.S2());
        }
    }
}
