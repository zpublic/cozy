using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy
{
    class Program
    {
        static void A()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("A - 核心C#");
            Console.WriteLine("-----------------------------------------------");
            LearnCSharp.A.A.Cozy();
        }

        static void B()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("B - 对象和类型");
            Console.WriteLine("-----------------------------------------------");
            LearnCSharp.B.B.Cozy();
        }

        static void C()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("C - 继承");
            Console.WriteLine("-----------------------------------------------");
            LearnCSharp.C.C.Cozy();
        }

        static void D()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("D - 泛型");
            Console.WriteLine("-----------------------------------------------");
            LearnCSharp.D.D.Cozy();
        }

        static void E()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("E - 数组");
            Console.WriteLine("-----------------------------------------------");
            LearnCSharp.E.E.Cozy();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("hello cozy!");
            Console.WriteLine("===============================================");

            //A();
            //B();
            //C();
            D();
            E();
        }
    }
}
