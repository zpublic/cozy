using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.D
{
    class D5GenericMethod
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Generic_Methods_Example();
            Generic_Methods_with_Constraints();
            Generic_Methods_with_Delegates();
        }

        // 定义泛型方法
        public static void swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        // 定义接口
        public interface LessInterface
        {
            bool less(LessInterface x);

            int Value
            {
                get;
                set;
            }
            
        }

        // 继承接口
        public class LessClass : LessInterface
        {
            public LessClass()
            {
                Value = 0;
            }

            public LessClass(int n)
            {
                Value = n;
            }

            // 实现接口
            private int _value = 0;

            public int Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = Value;
                }
            }
            public bool less(LessInterface x)
            {
                return Value < x.Value;
            }
        }

        // 带有约束的泛型方法
        public static bool less<T>(T a, T b)
            where T : LessInterface
        {
            return  a.less(b);
        }

        public static void Generic_Methods_Example()
        {
            int a = 1;
            int b = 2;
            swap(ref a, ref b);
            Console.WriteLine("{0} {1}", a, b);
        }

        public static void Generic_Methods_with_Constraints()
        {
            LessClass ls1 = new LessClass();
            LessClass ls2 = new LessClass();
            Console.WriteLine(less(ls1, ls2));
        }

        public static T GenericDelegates<T>(T element, Func<T,T> action)
        {
            return action(element);
        }

        public static void Generic_Methods_with_Delegates()
        {
            //带委托的泛型方法
            Console.WriteLine(GenericDelegates<int>(41, a => a + 1));
        }
    }
}
