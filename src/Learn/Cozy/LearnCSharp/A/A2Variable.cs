using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.A
{
    class A2Variable
    {
        //类型级别的声明 看做字段
        public static int SomeVar = 1;

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Initialization_of_Variables();
            Type_Inference();
            Variable_Scope();
            Constants();
        }

        public static void Initialization_of_Variables()
        {
            // 变量声明
            int a;
            a = 1;
            Console.WriteLine(a);

            // 声明并初始化
            int b = 2;
            Console.WriteLine(b);

            // 声明并初始化多个变量

            int ma = 10, mb = 20;
            Console.WriteLine(ma);
            Console.WriteLine(mb);

            // 使用局部变量时必须在显示初始化之后
            int no_init;
            no_init = 42;   // 如果不初始化就使用会产生错误
            Console.WriteLine(no_init);
        }

        public static void Type_Inference()
        {
            // 类型推导
            var c = 3;
            var str = "123";
            Type typeC = c.GetType();
            Type typeStr = str.GetType();
            Console.WriteLine(c);
            Console.WriteLine(str);
            Console.WriteLine(typeC.ToString());
            Console.WriteLine(typeStr.ToString());

            /*
             *      类型推断的变量必须初始化
             *      例: var noinit;      // 错误
             */
        }

        public static void Variable_Scope()
        {
            //      作用域
            { //    作用域开始

                int x = 10; // 作用域内定义变量
                Console.WriteLine(x);

            } //    作用域结束

            {
                int y = 10;
                Console.WriteLine(y);
                // int y = 20;   同一个作用域内不可以定义多个同名变量
            }

            {
                // 不同同级定义域中可以定义同名变量
                {
                    int x = 1;
                    Console.WriteLine("内层定义域A定义的x变量： " + x.ToString());
                }
                {
                    int x = 2;
                    Console.WriteLine("内层定义域B定义的x变量： " + x.ToString());
                }
                /*
                 * 外层定义域定义的变量不能在内层定义域中重新定义
                 * 例: int x = 0     // 错误
                 */
            }

            // 声明与静态字段同名的局部变量
            int SomeVar = 233;
            Console.WriteLine(SomeVar);                 // 输出局部变量SomeVar = 233
            Console.WriteLine(A2Variable.SomeVar);      // 输出静态字段SomeVar = 1
        }

        public static void Constants()
        {
            // 常量
            const int d = 4;
            // d = 3; // 错误 常量初始化之后不可更改
            Console.WriteLine(d);

            /*
             * int var1 = 1;
             * int var2 = 2;
             * const int e = var1 + var2;  // 错误 常量不可以用变量初始化
             */
        }
    }
}
