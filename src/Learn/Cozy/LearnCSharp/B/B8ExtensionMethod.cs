using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.B
{
    class B8ExtensionMethod
    {        
       public static void Cozy() {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            var myClass = new MyClass();
            myClass.SayHello();
            Console.WriteLine(string.Format("使用拓展方法反转Name字段： Name：{0}", myClass.ReverseMyName()));

            //调用string的拓展方法
            "".SayHelloForStringType();
        }
    }

    public class MyClass
    {
        public string Name = "B8ExtensionMethod";
    }

    public static class MyClassExtensionMethod
    {
        /// <summary>
        /// 给MyClass增加一个SayHello()的拓展方法
        /// </summary>
        /// <param name="myClass">要拓展的类型</param>
        public static void SayHello(this MyClass myClass)
        {
            Console.WriteLine("Hello for B8ExtensionMethod");
        }

        /// <summary>
        /// 使用MyClass实例的拓展方法应用
        /// </summary>
        /// <param name="myClass"></param>
        /// <returns></returns>
        public static string ReverseMyName(this MyClass myClass)
        {
            return string.Join("", myClass.Name.Reverse());
        }

        /// <summary>
        /// 给.NET自带类型增加拓展方法
        /// </summary>
        /// <param name="s"></param>
        public static void SayHelloForStringType(this string s)
        {
            Console.WriteLine("Hello for string");
        }
    }
}
