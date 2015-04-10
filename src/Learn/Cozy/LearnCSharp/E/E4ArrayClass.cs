using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{

    class RefObject : IComparable<RefObject>
    {
        public int value;

        // 实现IComparable<T> 接口
        public int CompareTo(RefObject other)
        {
            return this.value.CompareTo(other.value);
        }
    }

    class E4ArrayClass
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Creating_Arrays();
            Copying_Arrays();
            Sorting();
        }

        public static void Creating_Arrays()
        {
            // 创建类型为int 大小为3的数组
            Array intArray = Array.CreateInstance(typeof(int), 3);
            for (int i = 0; i < 3; ++i)
            {
                // 设置对应位置的值
                intArray.SetValue(42, i);
            }
            for(int i = 0; i < 3; ++i)
            {
                // 读取对应位置的值
                Console.Write(intArray.GetValue(i));
            }
            Console.WriteLine();

            // 强制转换为int[]的数组
            int[] intArray1 = (int[])intArray;
            foreach(var elemt in intArray1)
            {
                Console.Write(elemt);
            }
            Console.WriteLine();

            // 声明一个2*3的数组 第一维基于1 第二维基于10
            int[] length = { 2, 3 };
            int[] lowerBounds = { 1, 10 };
            Array racers = Array.CreateInstance(typeof(int), length, lowerBounds);

            racers.SetValue(0, 1, 10);  // 对应 racers[1][10]
            racers.SetValue(1, 1, 11);
            racers.SetValue(2, 1, 12);

            racers.SetValue(3, 2, 10);
            racers.SetValue(4, 2, 11);
            racers.SetValue(5, 2, 12);

            // 强制转换为int[,]类型
            int[,] racers2 = (int[,])racers;
            Console.WriteLine("{0} {1} {2}", racers2[1, 10], racers2[1, 11], racers2[1, 12]);
            Console.WriteLine("{0} {1} {2}", racers2[2, 10], racers2[2, 11], racers2[2, 12]);
        }

        public static void Copying_Arrays()
        {
            int[] intArray1 = { 1, 2 };
            // 调用Clone方法创建浅表副本
            int[] intArray2 = (int[])intArray1.Clone();
            int[] intArray3 = { 0 ,0 };
            // 调用Array.Copy创建浅表副本
            Array.Copy(intArray1, intArray3, intArray1.Length);

            foreach(var elemt in intArray2)
            {
                Console.Write(elemt);
            }
            Console.WriteLine();

            foreach (var elemt in intArray3)
            {
                Console.Write(elemt);
            }
            Console.WriteLine();

            // 创建引用类型的数组
            RefObject[] refArray1 = 
            {
                new RefObject { value = 0 },
                new RefObject { value = 1 }  
            };

            // 只复制引用
            RefObject[] refArray2 = (RefObject[])refArray1.Clone();

            foreach (var elemt in refArray1)
            {
                Console.Write(elemt.value);         // 输出 0 1
            }
            Console.WriteLine();
            foreach (var elemt in refArray1)
            {
                Console.Write(elemt.value);         // 输出 0 1
            }
            Console.WriteLine();

            // 修改堆中的对象
            refArray1[0].value = 2;
            refArray1[1].value = 3;

            foreach (var elemt in refArray1)
            {
                Console.Write(elemt.value);         // 输出 2 3
            }
            Console.WriteLine();
            foreach (var elemt in refArray1)
            {
                Console.Write(elemt.value);         // 输出 2 3
            }
            Console.WriteLine();
        }

        public static void Sorting()
        {
            RefObject[] refArray = 
            {
                new RefObject {value = 3},
                new RefObject {value = 2},
                new RefObject {value = 1},
                new RefObject {value = 9},
                new RefObject {value = 6}
            };

            foreach (var elemt in refArray)
            {
                Console.Write(elemt.value);
            }
            Console.WriteLine();

            // 排序
            Array.Sort(refArray);

            foreach (var elemt in refArray)
            {
                Console.Write(elemt.value);
            }
            Console.WriteLine();
        }
    }
}
