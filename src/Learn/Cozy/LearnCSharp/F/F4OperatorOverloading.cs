using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.F
{
    // 定义重载的类
    class OperatorClass
    {
        public int value;

        // 用int构造
        public OperatorClass(int n)
        {
            this.value = n;
        }

        // 拷贝构造
        public OperatorClass(OperatorClass rhs)
        {
            this.value = rhs.value;
        }

        // 重写 方便输出
        public override string ToString()
        {
            return "Value: " + value.ToString();
        }

        // 重载operator+
        public static OperatorClass operator +(OperatorClass lhs, OperatorClass rhs)
        {
            OperatorClass oc = new OperatorClass(lhs);
            oc.value += rhs.value;
            return oc;
        }

        public static OperatorClass operator +(OperatorClass lhs, int rhs)
        {
            OperatorClass oc = new OperatorClass(lhs);
            oc.value += rhs;
            return oc;
        }

        public static OperatorClass operator +(int lhs, OperatorClass rhs)
        {
            OperatorClass oc = new OperatorClass(rhs);
            oc.value += lhs;
            return oc;
        }

        // 重载operator==必须重载operator!=
        public static bool operator==(OperatorClass lhs, OperatorClass rhs)
        {
            return lhs.value == rhs.value;
        }

        public static bool operator !=(OperatorClass lhs, OperatorClass rhs)
        {
            return !lhs.Equals(rhs);
        }

        // 重载operator==和operator!=必须重载Equals和GetHashCode
        public override bool Equals(object obj)
        {
            return (this == (obj as OperatorClass));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    class F4OperatorOverloading
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            SimpleOperatorOverload();
        }

        public static void SimpleOperatorOverload()
        {
            OperatorClass op1 = new OperatorClass(1);
            OperatorClass op2 = new OperatorClass(1);

            // 调用重载的操作符
            OperatorClass op3 = op1 + 1;
            OperatorClass op4 = 1 + op1;
            OperatorClass op5 = op1 + op2;

            Console.WriteLine("op1 " + op1.ToString());
            Console.WriteLine("op2 " + op2.ToString());
            Console.WriteLine("op3 " + op3.ToString());
            Console.WriteLine("op4 " + op4.ToString());
            Console.WriteLine("op5 " + op5.ToString());

            // operator+=自动调用operator+的重载
            op4 += 3;
            op5 += op4;
            Console.WriteLine("op4 " + op4.ToString());
            Console.WriteLine("op5 " + op5.ToString());

            // 调用重载的operator==和operator!=
            bool b1 = op4 == op5;
            bool b2 = op4 != op5;
            bool b3 = op1 == op2;
            Console.WriteLine("{0} {1} {2}", b1, b2, b3);
        }
    }
}
