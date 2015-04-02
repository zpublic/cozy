using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.D
{

    // 定义泛型类
    class GenericClass<T>
    {
        public T GetObject()
        {
            // 初始化为默认值 引用类型为null 值类型为0
            T obj = default(T);
            return obj;
        }
    }

    // 声明接口
    interface Interface3
    {
        string DoSomething();
    }

    // 实现接口
    class InterfaceClass3 : Interface3
    {
        public string DoSomething()
        {
            return "InterfaceClass3";
        }
    }

    // 带约束的泛型类
    class InterfaceGenericClass<T>
        where T : Interface3, new()            // 约束类型必须实现Interface3接口 并且有一个默认构造函数
    {
        T obj = new T();

        public string GetValue()
        {
            return obj.DoSomething();
        }
    }

    // 泛型基类
    class GenericBase<T>
    {
        public virtual string DoSomeThing()
        {
            return "Base : " + typeof(T).ToString();
        }
    }

    // 继承泛型基类 基类类型必须相同或指定基类具体类型
    class GenericDerive<T> : GenericBase<T>
    {
        // 重写基类虚方法
        public override string DoSomeThing()
        {
            return "Derive : " + typeof(T).ToString() + "Base : " + base.DoSomeThing();
        }
    }

    // 定义一个有静态成员的泛型类
    class GenericClassStaticValue<T>
    {
        public static int value = 0;
    }

    class D2GenericFeatures
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Default_Values();
            Constraints();
            Inheritance();
            Static_Members();
        }

        public static void Default_Values()
        {
            GenericClass<int> gci = new GenericClass<int>();
            gci.GetObject();
        }

        public static void Constraints()
        {
            // 以InterfaceClass3实例化InterfaceGenericClass 满足约束
            InterfaceGenericClass<InterfaceClass3> ifgc1 = new InterfaceGenericClass<InterfaceClass3>();
            Console.WriteLine(ifgc1.GetValue());

            // InterfaceGenericClass<int> ifgci = new InterfaceGenericClass<int>(); // 不满足约束 不可以实例化
        }

        public static void Inheritance()
        {
            // 用int实例化GenericDerive
            GenericBase<int> gbi1 = new GenericDerive<int>();
            Console.WriteLine(gbi1.DoSomeThing());
        }

        public static void Static_Members()
        {
            // 用不同的类型实例化GenericClassStaticValue
            GenericClassStaticValue<int>.value = 1;
            GenericClassStaticValue<float>.value = 2;
            Console.WriteLine(GenericClassStaticValue<int>.value);
            Console.WriteLine(GenericClassStaticValue<float>.value);
        }
    }
}
