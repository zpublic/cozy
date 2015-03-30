﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    class A2Variable
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // 变量声明
            int a;
            a = 1;
            Console.WriteLine(a);

            // 声明并初始化
            int b = 2;
            Console.WriteLine(b);

            // 类型推导
            var c = 3;
            Console.WriteLine(c);

            // 常量
            const int d = 4;
            Console.WriteLine(d);
        }
    }
}