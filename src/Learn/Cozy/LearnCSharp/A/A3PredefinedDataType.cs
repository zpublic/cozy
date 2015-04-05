using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.A
{
    //引用类型定义
    class RefType
    {
        public int value;
    }

    //值类型定义
    struct ValueType
    {
        public int value;
    }

    class A3predefinedDataType
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Value_Types_and_Reference_Types();
            Predefined_Value_Types();
            Predefined_Reference_Types();
        }

          
        public static void Value_Types_and_Reference_Types()
        {
            // 引用类型和值类型

            // 声明值类型并初始化
            int i = 0;
            ValueType value_type;
            value_type.value = 1;
            Console.WriteLine(i);                   //输出 0
            Console.WriteLine(value_type.value);    //输出 1

            // 复制值类型
            int i_copy = i;
            ValueType value_copy = value_type;
            Console.WriteLine(i_copy);              // 输出 0
            Console.WriteLine(value_copy.value);    // 输出 1

            // 修改复制之后的值类型副本 副本被修改 原值类型不变
            i_copy = 1;
            value_copy.value = 2;
            Console.WriteLine(i);                   // 输出 0
            Console.WriteLine(i_copy);              // 输出 1
            Console.WriteLine(value_type.value);    // 输出 1
            Console.WriteLine(value_copy.value);    // 输出 2

            // 声明并初始化引用类型
            RefType ref_type = new RefType();       // 在托管堆上创建引用类型 需要用 new 关键字
            ref_type.value = 1;
            Console.WriteLine(ref_type.value);      // 输出 1

            // 复制引用类型
            RefType ref_copy = ref_type;            // 将ref_copy指向托管堆上创建的对象
            Console.WriteLine(ref_copy.value);      // 输出 1

            // 修改引用类型 副本和原变量全部改变
            ref_copy.value = 2;
            Console.WriteLine(ref_type.value);      // 输出 2
            Console.WriteLine(ref_copy.value);      // 输出 2
            ref_type.value = 3;
            Console.WriteLine(ref_type.value);       // 输出 3
            Console.WriteLine(ref_copy.value);       // 输出 3

            // 将引用设置为null
            ref_type = null;
            // ref_type.value = 42; 在运行期抛出一个异常

        }

        public static void Predefined_Value_Types()
        {
            // 预定义的值类型

            /*
             * 整数类型:
             * sbyte    System.SByte    8位有符号整数
             * byte     System.Byte     8位无符号整数
             * short    System.Int16    16位有符号整数
             * ushort   System.UInt16   16位无符号整数
             * int      System.Int32    32位有符号整数
             * uint     System.UInt32   32位无符号整数
             * long     System.Int64    64位有符号整数
             * ulong    System.UInt64   64位无符号整数
             */
             sbyte _sbyte = 0;
             byte _byte = 0;
             short _short = 0;
             ushort _ushort = 0;
             int _int = 0;
             uint _uint = 0U;
             long _long = 0L;
             ulong _ulong = 0UL;
             Console.Write(_sbyte);
             Console.Write(_byte);
             Console.Write(_short);
             Console.Write(_ushort);
             Console.Write(_int);
             Console.Write(_uint);
             Console.Write(_long);
             Console.Write(_ulong);

            /*
             * 浮点类型:
             * float    System.Single   32位单精度浮点数
             * double   System.Double   64位双精度浮点数
             */
             float _float = 0.0F;
             double _double = 0.0;
             Console.Write(_float);
             Console.Write(_double);

            /*
             * decimal类型:
             * decimal  System.Decimal  128位高精度十进制数表示法
             */
             decimal _decimal = 0.0M;
             Console.Write(_decimal);

            /*
             * bool类型:
             * bool     System.Boolean  true或false
             */
             bool _bool_true = true;
             bool _bool_false = false;
             Console.Write(_bool_true);
             Console.Write(_bool_false);

            /*
             * 字符类型:
             * char     System.Char     16位的(Unicode)字符
             * 
             * 转义序列
             * 
             * 单引号      \'
             * 双引号      \"
             * 反斜杠      \\
             * 空          \0
             * 警告        \a
             * 退格        \b
             * 换页        \f
             * 换行        \n
             * 回车        \r
             * 水平制表符  \t
             * 垂直制表符  \v
             * 
             */
             
             char _char = '\u0041';
             Console.Write(_char);
             Console.Write('\n');

        }

        public static void Predefined_Reference_Types()
        {
            /*
             * 预定义的引用类型
             * 
             * object   System.Object   根类型,CTS中的其他类型都是从它派生而来的(包括值类型)
             * string   System.String   Unicode字符串
             * 
             */

            // object类型是任何类型的父类 可以绑定任何子类型的对象
            int a = 0;
            object objA = a;
            float b = 0.0F;
            object objB = b;
            decimal Dec = 1.0M;
            object objDec = Dec;
            string str = "string";
            object objStr = str;

            // object实现了基本方法
            objA.Equals(objB);
            objB.GetHashCode();
            objStr.GetType();
            objDec.ToString();

            // string类型

            // 声明并初始化string类型
            string str1 = "hello";
            string str2 = "C#";
            Console.WriteLine(str1);                // 输出 hello
            Console.WriteLine(str2);                // 输出 C#

            // 连接string类型
            string str3 = str1 + " " + str2;
            Console.WriteLine(str3);                // 输出 hello C#

            // 改变string类型的值
            string str1_copy = str1;
            Console.WriteLine(str1_copy);           // 输出 hello
            str1 = "hello change";
            Console.WriteLine(str1);                // 输出 hello change
            Console.WriteLine(str1_copy);           // 输出 hello
            Console.WriteLine(str3);                // 输出 hello C#

            // 带有转义的字符串
            string convStr = "\\hello\\";
            Console.WriteLine(convStr);             // 输出 \hello\
            string noconvStr = @"\hello\";
            Console.WriteLine(noconvStr);           // 输出 \hello\
        }
    }
}
