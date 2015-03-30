using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.B
{
    public class B8ExtensionMethod {
        public string Name = "B8ExtensionMethod";

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");
            
            var b8ExtensionMethod = new B8ExtensionMethod();
            b8ExtensionMethod.SayHello();
            Console.WriteLine(string.Format("使用拓展方法反转Name字段： Name：{0}", b8ExtensionMethod.ReverseMyName()));

            //调用string的拓展方法
            "".SayHelloForStringType();
        }
    }

    public static class CozyExtensionMethod
    {
        /// <summary>
        /// 给B8ExtensionMethod增加一个SayHello()的拓展方法
        /// </summary>
        /// <param name="b8ExtensionMethod">要拓展的类型</param>
        public static void SayHello(this B8ExtensionMethod b8ExtensionMethod) 
        {
            Console.WriteLine("Hello for B8ExtensionMethod");           
        }

        /// <summary>
        /// 使用B8ExtensionMethod实例的拓展方法应用
        /// </summary>
        /// <param name="b8ExtensionMethod"></param>
        /// <returns></returns>
        public static string ReverseMyName(this B8ExtensionMethod b8ExtensionMethod) 
        {
            return string.Join("", b8ExtensionMethod.Name.Reverse());
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
