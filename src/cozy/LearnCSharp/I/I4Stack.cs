using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.I
{
    class I4Stack
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //栈和队列相反，遵循先进后出的原则
            //创建一个存放int类型的栈
            var stack = new Stack<int>();

            //入栈
            stack.Push(1);
            stack.Push(2);

            //访问栈顶的元素，但不出栈
            var i = stack.Peek();

            //出栈
            i = stack.Pop();
           
            //判断元素1是否在栈里面
            var isContains = stack.Contains(i);

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            //迭代所有元素，读取顺序是从最后一个压栈的元素开始
            foreach (var item in stack) {
                Console.WriteLine(item);
            }
        }
    }
}
