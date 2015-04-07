using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I8Set
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //HashSet<T>和List<T>差不多的，但HashSet<T>只能包含不重复的元素
            var hashSet = new HashSet<int>();

            //Add()会返回一个bool值,添加成功返回true,集合内有相同的元素，则添加失败返回false
            Console.WriteLine(hashSet.Add(1));     //output True
            Console.WriteLine(hashSet.Add(1));     //output False

            Console.WriteLine(hashSet.Add(2));
            Console.WriteLine(hashSet.Add(3));

            var array = new[] {1, 2, 3, 4, 5};


            Console.WriteLine(hashSet.IsSubsetOf(array));       //output True
            Console.WriteLine(hashSet.IsSupersetOf(array));     //output False

            //增加array集合的元素
            hashSet.UnionWith(array);

            foreach (var i in hashSet)
            {
                Console.WriteLine(i);
            }

            //移除array集合的元素
            hashSet.ExceptWith(array);

            foreach (var i in hashSet)
            {
                Console.WriteLine(i);
            }
        }
    }
}
