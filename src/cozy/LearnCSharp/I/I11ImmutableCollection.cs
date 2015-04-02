using System;
using System.Collections.Immutable;

namespace cozy.LearnCSharp.I
{
    class I11ImmutableCollection
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //不可变集合主要应用在多线程开发中资源共享的问题
            //不可变集合里面的元素都是只读
            //创建一个不可变的集合，Immutable类型都没有构造函数，使用Create()创建
            var immutableList = ImmutableList.Create<int>(1, 2, 3, 4, 5, 6);

            //添加一个元素
            immutableList.Add(1);

            //添加一个集合
            immutableList.AddRange(new[] {10, 20, 30, 40});

            //移除一个元素
            immutableList.Remove(4);

            immutableList.ForEach(Console.WriteLine);
        }
    }
}
