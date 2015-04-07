using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Cozy.LearnCSharp.I
{
    class I9ObservableCollection
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            var observableList = new ObservableCollection<string>();
            observableList.CollectionChanged += (s, e) =>
            {
                Console.WriteLine("执行的操作是{0}", e.Action);
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Console.WriteLine("新增了一个元素：{0}", ((IList<string>)s)[e.NewStartingIndex]);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Console.WriteLine("移除了一个元素:{0}", e.OldItems[e.OldStartingIndex]);
                        break;
                    case NotifyCollectionChangedAction.Move:
                        Console.WriteLine("移动了一个元素:{0}", e.OldItems[e.OldStartingIndex]);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        Console.WriteLine("替换了一个元素:{0}", e.OldItems[e.OldStartingIndex]);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        Console.WriteLine("集合的内容发生显著更改");
                        break;
                    default:
                        return;
                }
            };

            observableList.Add("Hello");
            observableList.Add("world");
            observableList.Move(0, 1);
            observableList[0] = "Replace";
            observableList.Remove("Replace");
            observableList.Clear();
        }
    }
}
