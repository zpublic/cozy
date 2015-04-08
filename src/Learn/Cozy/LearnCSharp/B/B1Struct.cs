using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.B
{
    class B1Struct
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //结构在性能上面要优于类，因为结构是值类型，值类型的值都存储在栈里面
            //但在C#里面，结构类型比较少用，因为托管堆的优化，即使是存储在托管堆里面的引用类型，这点性能损耗也是极小的
            //结构类型不支持继承

            //因为结构是值类型，不new也没问题，并且new关键字只用于调用构造函数，并不产生实例引用
            Dimensions dimensions;
            dimensions.Length = 20;
            dimensions.Width = 20;
            dimensions.Func();
            dimensions = new Dimensions();
            dimensions = new Dimensions(20, 20);
        }
    }

    
    struct Dimensions {

        public double Length;
        public double Width;

        //结构类型不允许无参构造函数
        //public Dimensions()
        //{            
        //}

        //如果带参数，必须对所有字段赋值
        //public Dimensions(int length)
        //{
        //}

        //正确的构造函数
        public Dimensions(int length, int width)
        {
            this.Length = length;
            this.Width = width;
        }

        public void Func()
        {
            Console.WriteLine("Hello,world");
        }
    }
}
