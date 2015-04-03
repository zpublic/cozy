using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.C
{
    class ModifierClass
    {
        // 定义被修饰符修饰的成员
        public static int intPub = 0;
        protected static int intPro = 1;
        internal static int intInt = 2;
        private static int intPri = 3;

        public static void ModifierMethod()
        {
            // 在定义的类内部所有修饰符修饰的成员都可以访问
            Console.WriteLine("ModifierClass");
            Console.WriteLine(intPub);
            Console.WriteLine(intInt);
            Console.WriteLine(intPro);
            Console.WriteLine(intPri);
            Console.WriteLine(InnerClass.intPubInner);
            // Console.WriteLine(InnerClass.intPriInner);       // 外部类访问内部类按照正常访问规则
        }

        // 嵌套类型可以被修饰
        protected class InnerClass
        {
            public static int intPubInner = 4;
            private static int intPriInner = 5;

            public static void InnerMethod()
            {
                // 内部类可以访问外部类的所有成员
                Console.WriteLine("InnerClass");
                Console.WriteLine(intPub);
                Console.WriteLine(intInt);
                Console.WriteLine(intPro);
                Console.WriteLine(intPri);
                Console.WriteLine(intPubInner);
                Console.WriteLine(intPriInner);
            }
        }
    }

    // 继承基类
    class ModifierClassDerive : ModifierClass
    {
        public new static void ModifierMethod()
        {
            Console.WriteLine("ModifierClassDerive");
            Console.WriteLine(intPub);
            Console.WriteLine(intInt);
            Console.WriteLine(intPro);                          // protected修饰的成员在派生类可以访问
            // Console.WriteLine(intPri);                       // private修饰的成员在派生类不可以访问

            // 调用基类中内部类的public静态方法
            InnerClass.InnerMethod();                           // protected修饰的内部类在派生类可以访问
        }
    }

    class C3Modifier
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Visibility_Modifiers();
            Other_Modifiers();
        }

        public static void Visibility_Modifiers()
        {
            Console.WriteLine(ModifierClass.intPub);
            Console.WriteLine(ModifierClass.intInt);
            // Console.WriteLine(intPro);                           // protected修饰的成员在其他代码中不可以访问
            // Console.WriteLine(intPri);                           // private修饰的成员在其他代码中不可以访问

            ModifierClass.ModifierMethod();
            ModifierClassDerive.ModifierMethod();
        }

        public static void Other_Modifiers()
        {
            // new static virtual abstract override sealed修饰符参考C2ImplementInheritance部分
            Console.WriteLine("Hello Other Modifiers");
        }
    }
}
