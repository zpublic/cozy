using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Cozy.LearnCSharp.N
{
    // 用AttributeUsage修饰自定义特性CozyTest
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
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

        public string Name
        {
            get
            {
                return name;
            }
        }

        // 特性的构造函数 接受一个string类型
        public CozyTestAttribute(string str)
        {
            name = str;
        }
    }

    // 定义一个用于属性或字段的特性 用于标志最小值和最大值
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    class CozyMinMaxAttribute : Attribute
    {
        public int minValue
        {
            get;
            private set;
        }

        public int maxValue
        {
            get;
            private set;
        }

        public CozyMinMaxAttribute(int min, int max)
        {
            this.minValue = min;
            this.maxValue = max;
        }
    }

    class CozyTestClass
    {
        // 使用特性CozyMinMax
        [CozyMinMax(0, 100)]
        public int TestValue
        {
            get;
            set;
        }

        [CozyMinMax(0, 10)]
        public int TestState
        {
            get;
            set;
        }

        // 使用特性CozyTest
        [CozyTest("Cozy Test Method", Id = 1)]
        public static void Test()
        {

        }

        [CozyTest("Cozy DoSomething Method", Id = 2)]
        public void DoSomething()
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
            Use_by_Attribute_CozyMinMax();
        }

        public static void Writing_Custom_Attributes()
        {
            Type t = typeof(CozyTestClass);

            // 获取CozyTestClass所有方法
            MethodInfo[] mi = t.GetMethods();
            foreach (var obj in mi)
            {
                // 遍历的过程中判断是否有CozyTest特性
                var objs = obj.GetCustomAttribute(typeof(CozyTestAttribute), false) as CozyTestAttribute;
                if (objs != null)
                {
                    Console.WriteLine("Method : " + obj.DeclaringType.Name + "::" + obj.Name + " Attribute : " + objs.Name);
                }
            }
        }

        // 判断CozyMinMax特性之后输出CozyTestClass类中TestValue的值
        public static void Use_by_Attribute_CozyMinMax()
        {
            CozyTestClass ctc = new CozyTestClass();
            ctc.TestValue = 101;
            Print_the_Result_of_Attribute(ctc);
        }

        public static void Print_the_Result_of_Attribute(CozyTestClass n)
        {
            Type typeInfo = typeof(CozyTestClass);
            PropertyInfo CozyProInfo = typeInfo.GetProperty("TestValue");
            var CozyProAttrib = CozyProInfo.GetCustomAttribute<CozyMinMaxAttribute>() as CozyMinMaxAttribute;
            int result = 0;
            if (CozyProAttrib != null)
            {
                int minValue = CozyProAttrib.minValue;
                int maxValue = CozyProAttrib.maxValue;
                if (n.TestValue < minValue)
                {
                    result = minValue;
                }
                else if (n.TestValue > maxValue)
                {
                    result = maxValue;
                }
            }
            else
            {
                result = n.TestValue;
            }
            Console.WriteLine("{0} is {1}", CozyProInfo.Name, result);
        }
    }
}
