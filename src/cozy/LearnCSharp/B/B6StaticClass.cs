using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.B 
{
    class B6StaticClass 
    {
        public static void Cozy() 
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //静态类不能被实例化,注释代码不能通过编译
            //var staticClass = new StaticClass();

            //静态类内的静态成员可以直接通过类型来访问
            StaticClass.StaticFunc();
            StaticClass.StaticProperty = string.Empty;
        }
    }

    static class StaticClass 
    {
        public static string StaticProperty;

        public static void StaticFunc() 
        {
        }

        //静态类内不允许包含实例成员，注释代码不能通过编译
        // public string InstanceProperty;
        //void InstanceFunc() 
        //{
        //}

    }
}
