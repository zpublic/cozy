using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.C
{

    // 定义接口
    interface Interface
    {
        // 定义结构的方法
        string InterfaceDo();

        // 定义属性
        int Value
        {
            get;
        }

        // 定义接口
        void add(int n);

        void sub(int n);

    }

    // 继承接口的类
    class InterfaceClass1 : Interface
    {
        private int _value = 0;


        // 实现接口
        public string InterfaceDo()
        {
            return "Interface Class";
        }

        public void add(int n)
     
        {
            _value += n;
        }

        public void sub(int n)
        {
            _value -= n;
        }

        // 实现属性
        public int Value
        {
            get
            {
                return _value;
            }
        }
    }

    // 定义基接口
    interface BaseInterface
    {
        string BaseInterfaceDo();
    }

    // 继承基接口
    interface DeriveInterface : BaseInterface
    {
        string DeriveInterfaceDo();
    }

    // 实现基接口与继承接口的全部方法
    class DeriveClass : DeriveInterface
    {
        public string BaseInterfaceDo()
        {
            return "BaseInterfaceDo";
        }
        public string DeriveInterfaceDo()
        {
            return "DeriveInterfaceDo";
        }
    }

    class C4Interface
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Defining_and_Implementing_Interfaces();
            Derived_Interfaces();
        }

        public static void Defining_and_Implementing_Interfaces()
        {
            Interface ic = new InterfaceClass1();

            Console.WriteLine(ic.InterfaceDo());
            ic.add(1);
            Console.WriteLine(ic.Value);
            ic.sub(2);
            Console.WriteLine(ic.Value);
        }

        public static void Derived_Interfaces()
        {
            DeriveClass dc = new DeriveClass();
            Console.WriteLine(dc.BaseInterfaceDo());
            Console.WriteLine(dc.DeriveInterfaceDo());
        }
    }
}
