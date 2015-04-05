using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I6SortedList
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //需要根据键对集合排序的时候，就可以使用有序列表SortedList<TKey,TValue>
            //其他的用法和继承了 ICollection, IEnumerable的集合类型一样
            var sortedList = new SortedList<int, string>();

            sortedList.Add(10, "Msg10");
            sortedList.Add(9,"Msg9");
            sortedList.Add(11,"Msg11");
            sortedList.Add(1, "Msg1");
            
            foreach (var item in sortedList) {
                Console.WriteLine(item);
            }
        }
    }
}
