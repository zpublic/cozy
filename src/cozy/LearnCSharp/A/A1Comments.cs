using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    class A1Comments
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // 单行注释的写法
            // 111111111

            /*
             * 多行注释的写法
             * 呵呵哒
             */

            ///<summary>
            ///xml文档注释
            ///</summary>
        }
    }
}
