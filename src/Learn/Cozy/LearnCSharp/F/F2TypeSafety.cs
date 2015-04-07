using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.F
{
    class F2TypeSafety
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Implicit_Conversions();
            Explicit_Conversions();
            Boxing_an_Unboxing();
        }

        public static void Implicit_Conversions()
        {
            int i = 0;
            long l = i;         // int隐式转换为long
            float f = i;        // int隐式转换为float
            // byte b = i;      // int不能隐式转换为byte
            int? ni = i;        // 不空类型隐式转换为可空类型
            long? nl = ni;      // 可空类型按照不可空类型转换
            // int uni = ni;    // 可空类型不能隐式转换为不可空类型
        }

        public static void Explicit_Conversions()
        {
            int i = 42;
            byte b = (byte)i;
            Console.WriteLine(b);       // 输出42

            i = 257;
            b = (byte)i;
            Console.WriteLine(b);       // 输出1

            // b = checked((byte)i);    // 抛出异常
            i = 3;
            b = checked((byte)i);       // 正常
            Console.WriteLine(b);

            ushort us = 43;
            char c = (char)us;
            Console.WriteLine(c);       // 输出字符 '+'

            uint uci = 2147483648;
            int si = (int)uci;
            uint ui = (uint)si;
            Console.WriteLine("{0} {1}", si, ui);      
        }

        public static void Boxing_an_Unboxing()
        {
            int i = 42;
            object boxing = i;              // 装箱
            int unboxing = (int)boxing;     // 拆箱
            Console.WriteLine(boxing.ToString() + " {0}", unboxing);
        }
    }
}
