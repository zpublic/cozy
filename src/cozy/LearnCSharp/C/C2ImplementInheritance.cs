using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.C
{
    // 声明一个类
    class ClassType
    {

    }
    /*
     * 等价于
     * 
     * class ClassType : object
     * {
     *      ……
     * }
     * 
     */

    // 声明基类
    class Base
    {
        // 非虚方法
        public string noVirtualMethod()
        {
            return "Base No virtual method";
        }

        // 虚方法
        public virtual string VirtualMethod()
        {
            return "Base virtual method";
        }
    }

    // 继承基类
    class Derive1 : Base
    {
        // 重写基类虚方法
        public override string VirtualMethod()
        {
            return "Derive1 virtual method";
        }
    }

    // 继承基类
    class Derive2 : Base
    {
        // 隐藏基类非虚方法
        public new string noVirtualMethod()
        {
            return "Derive2 No virtual method";
        }

        // 重写基类虚方法
        public override string VirtualMethod()
        {
            return "Derive2 virtual method";
        }
    }

    // 继承基类
    class Derive3 : Base
    {
        // 隐藏基类虚方法
        public new virtual string VirtualMethod()
        {
            return "Derive3 virtual method";
        }
    }

    // 继承隐藏基类虚方法的派生类
    class Derive4 : Derive3
    {
        // 重写基类虚方法
        public override string VirtualMethod()
        {
            return "Derive4 virtual method";
        }
    }
    
    // 继承重写基类虚方法的派生类
    class Derive5 : Derive1
    {
        // 隐藏基类虚方法
        public new string VirtualMethod()
        {
            return "Derive5 virtual method";
        }
    }

    // 继承基类
    class Derive6 : Base
    {
        // 重写基类虚方法
        public override string VirtualMethod()
        {
            // 调用基类方法
            return "Derive6 " + base.VirtualMethod();
        }
    }

    // 声明抽象类
    abstract class BaseAbs
    {
        // 声明抽象函数
        public abstract string AbstractMethod();
    }

    class DeriveAbs :　BaseAbs
    {
        public override string AbstractMethod()
        {
            return "DeriveAbs AbstractMethod";
        }
    }

    class NormalBase
    {
        // 普通方法
        public virtual string NormalMethod()
        {
            return "NormalBase Method";
        }
    }

    // 继承基类
    class SealedDerive : NormalBase
    {
        // 重写并且封闭普通方法
        public sealed override string NormalMethod()
        {
            return "SealedDerive Method";
        }
    }

    class SealedDerive1 : SealedDerive
    {
        /*
         * // 无法重写密封方法
         * public override string NormalMethod()
         * {
         *      ……
         * }
         * 
         */
    }

    // 继承并封闭基类
    sealed class SealedDerive2 : NormalBase
    {
        public sealed override string NormalMethod()
        {
            return "SealedDerive2 Method";
        }
    }

    /*
     * 错误 无法继承密封类
     * class SealedDerive3 : SealedDerive2
     * {
     * 
     * }
     * 
     */

    class C2ImplementInheritance
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Virtual_Method();
            Hiding_Methods();
            Calling_Base_Versions_of_Functions();
            Abstract_Classes_and_Functions();
            Sealed_Classes_and_Methods();

        }

        public static void Virtual_Method()
        {
            Base b = new Base();
            Derive1 d1 = new Derive1();
            Base bd1 = new Derive1();

            Console.WriteLine(b.noVirtualMethod());                 // 输出 Base No virtual method
            Console.WriteLine(b.VirtualMethod());                   // 输出 Base virtual method
            Console.WriteLine(d1.noVirtualMethod());                // 输出 Base No virtual method
            Console.WriteLine(d1.VirtualMethod());                  // 输出 Derive1 virtual method
            Console.WriteLine(bd1.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(bd1.VirtualMethod());                 // 输出 Derive1 virtual method
        }

        public static void Hiding_Methods()
        {
            Derive2 d2 = new Derive2();
            Base bd2 = new Derive2();

            Console.WriteLine(d2.noVirtualMethod());                // 输出 Derive2 No virtual method
            Console.WriteLine(d2.VirtualMethod());                  // 输出 Derive2 virtual method
            Console.WriteLine(bd2.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(bd2.VirtualMethod());                 // 输出 Derive2 virtual method

            Derive3 d3 = new Derive3();
            Derive4 d4 = new Derive4();
            Base bd3 = new Derive3();
            Base bd4 = new Derive4();
            Derive3 dd3 = new Derive4();

            Console.WriteLine(d3.noVirtualMethod());                // 输出 Base No virtual method
            Console.WriteLine(d3.VirtualMethod());                  // 输出 Derive3 virtual method
            Console.WriteLine(d4.noVirtualMethod());                // 输出 Base No virtual method
            Console.WriteLine(d4.VirtualMethod());                  // 输出 Derive4 virtual method
            Console.WriteLine(bd3.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(bd3.VirtualMethod());                 // 输出 Base virtual method
            Console.WriteLine(bd4.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(bd4.VirtualMethod());                 // 输出 Base virtual method
            Console.WriteLine(dd3.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(dd3.VirtualMethod());                 // 输出 Derive4 virtual method

            Base bd5 = new Derive5();
            Console.WriteLine(bd5.noVirtualMethod());               // 输出 Base No virtual method
            Console.WriteLine(bd5.VirtualMethod());                 // 输出 Derive1 virtual method
        }

        public static void Calling_Base_Versions_of_Functions()
        {
            Derive6 d6 = new Derive6();
            Console.WriteLine(d6.VirtualMethod());                  // 输出 Derive6 Base virtual method
        }

        public static void Abstract_Classes_and_Functions()
        {
            // BaseAbs Babs = new BaseAbs();                        // 错误 抽象类不能实例化
            BaseAbs Abs1 = new DeriveAbs();
            DeriveAbs DAbs1 = new DeriveAbs();
            Abs1.AbstractMethod();                                  // 输出 DeriveAbs AbstractMethod
            DAbs1.AbstractMethod();                                 // 输出 DeriveAbs AbstractMethod
        }

        public static void Sealed_Classes_and_Methods()
        {
            NormalBase nb = new NormalBase();
            SealedDerive sd = new SealedDerive();
            SealedDerive1 sd1 = new SealedDerive1();
            SealedDerive2 sd2 = new SealedDerive2();

            Console.WriteLine(nb.NormalMethod());                   // 输出 NormalBase Method
            Console.WriteLine(sd.NormalMethod());                   // 输出 SealedDerive Method
            Console.WriteLine(sd1.NormalMethod());                  // 输出 SealedDerive Method
            Console.WriteLine(sd2.NormalMethod());                  // 输出 SealedDerive2 Method
        }
    }
}
