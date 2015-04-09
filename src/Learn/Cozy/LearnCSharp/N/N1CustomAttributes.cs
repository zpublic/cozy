using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.N
{
    // 用AttributeUsage修饰自定义特性CozyTest
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    class CozyTestAttribute : Attribute
    {
        private string name;

        // 属性Id 用于特性的可选参数
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        // 特性的构造函数 接受一个string类型
        public CozyTestAttribute(string str)
        {
            name = str;
        }
    }

    class CozyTestClass
    {
        // 使用特性CozyTest
        [CozyTest("cozy test", Id = 1)]
        public static void Test()
        {

        }
    }

    class N1CustomAttributes
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Writing_Custom_Attributes();
        }
    
        public static void Writing_Custom_Attributes()
        {
            CozyTestClass.Test();
        }
    }
}
