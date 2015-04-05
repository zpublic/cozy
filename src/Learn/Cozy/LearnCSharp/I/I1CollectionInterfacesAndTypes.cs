using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I1CollectionInterfacesAndTypes
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //IEnumerable<T> 实现了该接口的集合可以使用foreach语句的迭代
            IEnumerable<string> enumerable = new List<string>();

            //ICollection<T> 该接口定义元素的操作方法，如Add(),Remove()等等
            ICollection<string> collection = new List<string>();

            //IList<T> 实现了该接口的集合可以通过索引位置访问其中的元素列表
            IList<string> list = new List<string>();

            //List<T>都继承以上接口，这玩意很强大，.NET里面最常用的集合类型： List<T>

            //ISet 允许实现这个接口的集合合并不同的集，获得两个集的交集
            ISet<string> set = new HashSet<string>();

            //IDictionary 该接口包含k-v结构
            IDictionary<string,string> dictionary = new Dictionary<string, string>();

            //ILookup 类似IDictionary，但是是k-*v结构，一个键可以对应多个值
            ILookup<string, string> lookup = new List<string>().ToLookup(x => x);

            //IComparer<T> 由比较器去实现，该接口有一个Compare(),可以按该方法里面的实现来进行排序

            //IEqualityComparer<T> 同样有比较器去实现，但它可以用Key去进行比较排序

            //IProducerConsumerCollection<T> 实现该接口用来操作线程安全集合的方法
        }
    }
}
