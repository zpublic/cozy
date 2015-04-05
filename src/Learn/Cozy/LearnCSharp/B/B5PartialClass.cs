using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.B
{
    class B5PartialClass
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            
            new PartialClass().SayHello();
        }
    }

    /// <summary>
    /// Partial关键字可以允许一个类分散在多个文件
    /// </summary>
    partial class PartialClass
    {
         //定义一空类
        //SayHello()实现在 ~/Details/B5PartialClass.cs
    }
}
