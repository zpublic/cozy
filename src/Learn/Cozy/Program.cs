using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("hello cozy!");
            Console.WriteLine("===============================================");

            PlayLearnCSharp();
            //PlayLearnFoundation();

        }

        static void PlayLearnCSharp()
        {
            /*
            >> LearnCSharp  
            >>> A  - 核心C#  
            >>> B  - 对象和类型  
            >>> C  - 继承  
            >>> D  - 泛型  
            >>> E  - 数组和元组  
            >>> F  - 运算符和类型转换  
            >>> G  - 委托、lambda表达式和事件  
            >>> H  - 字符串和正则表达式  
            >>> I  - 集合  
            >>> J  - LINQ  
            >>> K  - 动态语言扩展  
            >>> L  - 异步编程  
            >>> M  - 内存管理和指针  
            >>> N  - 反射  
            >>> O  - 错误和异常  
            */
            //LearnCSharpPlayer.A();
            //LearnCSharpPlayer.B();
            //LearnCSharpPlayer.C();
            //LearnCSharpPlayer.D();
            //LearnCSharpPlayer.E();
            //LearnCSharpPlayer.F();
            //LearnCSharpPlayer.G();
            //LearnCSharpPlayer.H();
            //LearnCSharpPlayer.I();
            //LearnCSharpPlayer.J();
            LearnCSharpPlayer.K();
            //LearnCSharpPlayer.L();
            //LearnCSharpPlayer.M();
            //LearnCSharpPlayer.N();
            //LearnCSharpPlayer.O();
        }

        static void PlayLearnFoundation()
        {
            /*
            >> LearnFoundation  
            >>> A  - 任务、线程和同步  
            >>> B  - 安全性  
            >>> C  - 互操作  
            >>> D  - 文件和注册表操作  
            >>> E  - 网络  
            >>> F  - 本地化  
            >>> G  - XML处理  
            */
            LearnFoundationPlayer.A();
            LearnFoundationPlayer.B();
            LearnFoundationPlayer.C();
            LearnFoundationPlayer.D();
            LearnFoundationPlayer.E();
            LearnFoundationPlayer.F();
            LearnFoundationPlayer.G();
        }
    }
}
