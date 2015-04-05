using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.D
{
    // 声明泛型类    
    class Object10<T>
    {
        private T[] array = new T[10];

        public int ObjSize
        {
            get
            {
                return array.Length;
            }
        }

        public T GetObj(int n)
        {
            return array[n];
        }

        public void SetObj(int n, T value)
        {
            array[n] = value;
        }
    }
    class D1GenericClass
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Generic_Class();
        }

        public static void Generic_Class()
        {
            // 实例化泛型类
            Object10<int> o10 = new Object10<int>();

            Console.WriteLine(o10.ObjSize);
            Console.WriteLine(o10.GetObj(0));
            o10.SetObj(0,42);
            Console.WriteLine(o10.GetObj(0));
        }
    }
}
