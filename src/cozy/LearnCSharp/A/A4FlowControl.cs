using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    class A4FlowControl
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // if..语句
            bool conditionIf = false;
            if(conditionIf)
            {
                // 如果conditionIf为真执行
                Console.WriteLine("conditionIf为真");
            }

            // if...else...语句
            bool condition = false;
            if(condition)
            {
                // 如果condition为真执行
                Console.WriteLine("condition为真");
            }
            else
            {
                // 如果condition为假执行
                Console.WriteLine("condition为假");
            }

            // if..else if..else 语句
            int conditionInt = 0;
            if(conditionInt == 0)
            {
                // 如果满足conditionInt等于0执行
                Console.WriteLine("conditionInt为0");
            }
            else if(conditionInt == 1)
            {
                // 如果满足conditionInt等于1执行
                Console.WriteLine("conditionInt为1");
            }
            else if (conditionInt == 2)
            {
                // 如果满足conditionInt等于2执行
                Console.WriteLine("conditionInt为2");
            }
            else
            {
                // 如果conditionInt不等于0、1、2执行
                Console.WriteLine("conditionInt不为0、1、2");
            }

            // switch语句
            int integer = 0;
            switch(integer) // integer为测试的变量
            {
                case 1:
                    // 当 integer等于1执行
                    Console.WriteLine("integer 等于 1");
                    break;

                case 2:
                    // 当 integer等于2执行
                    Console.WriteLine("integer 等于 2");
                    break;

                case 3:
                    // 当 integer等于3执行
                    Console.WriteLine("integer 等于 3");
                    break;

                /* 不能有相同的case
                 * case 3:  //又一条case 3:
                 * Console.WriteLine("integer 等于 3");
                 * break;
                 */

                // 当一条case为空 则可以跳至下一条case
                // 当 integer等于4或5执行
                case 4:
                case 5:
                    Console.WriteLine("integer 等于 4 或 integer 等于 5");
                    break;

                /* 
                 * 如果一条case没有break会提示出错:
                 * case 6:
                 * Console.WriteLine("integer 等于 6");
                 */

                // 当 integer不等于给出的case执行
                default:
                    Console.WriteLine("integer 不等于 1、2、3、4、5");
                    break;
            }

            // 可以用string作为测试的变量
            string caseStr = "case";
            switch(caseStr)
            {
                case "case1":
                    Console.WriteLine("caseStr 等于 case1");
                    break;
                case "case2":
                    Console.WriteLine("caseStr 等于 case1");
                    break;
                default:
                    Console.WriteLine("caseStr 等于 其他");
                    break;
            }

            /*
             * for循环
             * 
             * for (initializer; condition; iterator):
             * statement(s)
             * 
             */

            // 输出从0到9的整数
            for(int i = 0 /* 将i初始化为0 */ ; i < 10 /* 判断i < 10 如果为假则停止 */ ; ++i /* 将i增加1 */)
            {
                Console.WriteLine(i);
            }

            // while循环
            int while_i = 0;
            while( while_i < 10) // 当while_i < 10 则循环
            {
                Console.WriteLine(while_i);
                ++while_i;  // 自增while_i
            }

            // do...while 语句 先执行一次 之后再判断
            bool do_while_if = false;
            do
            {
                Console.WriteLine("do_while");
            }
            while (do_while_if); // 判断do_while_if 是否为真 为真则继续循环

            // foreach语句
            int[] container = new int[10];   // 声明一个int数组
            for (int i = 0; i < 10; ++i )   // 给数组中的元素赋值
            {
                container[i] = i;
            }

            // 遍历集合中的元素
            foreach (int elemt in container)  // 用foreach遍历数组
            {
                Console.WriteLine(elemt);
                //elemt = 0; 错误 foreach不能修改集合中的值
            }

            // goto 语句 一般不建议使用
            goto Label1;    // 跳转到Label1标签的语句
            Label1:         // Label1标签
            Console.WriteLine("goto Label1");

            // break语句 跳出最内部循环
            for(int i = 0; i < 10; ++i)         // i从0循环到9
            {
                if (i == 5) break;              // 如果i等于5 则跳出
                Console.WriteLine(i);
            }
            // 输出的结果为 0 1 2 3 4

            // continue语句 // 立即开始下一次循环
            for (int i = 0; i < 10; ++i)        // i从0循环到9
            {
                if (i == 5) continue;           // 如果i等于5 则跳出
                Console.WriteLine(i);
            }
            // 输出的结果为 0 1 2 3 4 6 7 8 9

            // return语句 结束当前方法

        }
    }
}
