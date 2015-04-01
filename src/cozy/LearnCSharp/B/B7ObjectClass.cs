using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.B
{
    class B7ObjectClass
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //使用New关键创建一个实例
            var objectClass = new ObjectClass();

            //实例成员只有通过实例才能进行访问
            objectClass.Name = "Test";
            objectClass.InstanceFunc();

            //GetType()继承自Object类型
            objectClass.GetType();

            //静态成员不能通过实例来访问，下面代码不通过编译
            //objectClass.StaticName = "Test";
            //objectClass.StaticFunc();

            //静态成员只能通过类型去访问
            ObjectClass.StaticName = "Test";
            ObjectClass.StaticFunc();

            //实例成员不能通过类型来访问，下面代码不通过编译
            //ObjectClass.Name = "Test";
            //ObjectClass.InstanceFunc();
        }
    }

    /// <summary>
    /// .NET里面所有的类型都继承Object(包括自定义类型和基本类型)
    /// </summary>
    public class ObjectClass
    {
        public string Name;

        public void InstanceFunc()
        {
             Console.WriteLine("这是一个实例方法");
        }

        //非静态类既可以包含实例成员，也可以包含静态成员
        public static string StaticName;

        public static void StaticFunc()
        {
            Console.WriteLine("这是一个静态方法");    
        }
    }
}
