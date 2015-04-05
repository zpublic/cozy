using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.C
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

    interface Interface2
    {

    }

    // Class2实现继承Class1
    class Class2 : Class1
    {
        public override string S1()
        {
            return "class2";
        }
    }

    // Class3接口继承 Interface1
    class Class3 : Interface1
    {
        public string S2()
        {
            return "class3";
        }
    }

    // Class4多重继承自Class1和Interface1
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

    /*
     * 错误 类不能继承多个基类
     * 
     * class Class5 : Class1, Class2
     * {
     *     
     * }
     *
     */

    // 结构可以接口继承
    struct Struct1 : Interface1
    {
        public string S2()
        {
            return "struct1";
        }
    }


    // 结构可以继承任意多个接口
    struct Struct2 : Interface1, Interface2
    {
        public string S2()
        {
            return "struct2";
        }
    }

    /*
     * // 错误 结构支持实现继承
     * 
     * struct Struct3 : struct1
     * {
     *
     * }
     * 
     */

    class C1InheritanceType
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Implementation_Versus_Interface_Inheritance();
            Multiple_Inheritance();
            Structs_and_Classes();
        }

        public static void Implementation_Versus_Interface_Inheritance()
        {
            // 实现继承
            Class2 c2 = new Class2();
            Console.WriteLine(c2.S1());

            // 接口继承
            Class3 c3 = new Class3();
            Console.WriteLine(c3.S2());
        }

        public static void Multiple_Inheritance()
        {
            // 多重继承
            Class4 c4 = new Class4();
            Console.WriteLine(c4.S1());
            Console.WriteLine(c4.S2());
        }

        public static void Structs_and_Classes()
        {
            // 类继承自用户选择的另一个类 并且都继承自System.Object
            object classObj = new Class1();

            // 结构继承自System.ValueType 并且System.ValueType继承自System.Object
            ValueType structValue = new Struct1();
            object structObj = structValue;
        }
    }
}
