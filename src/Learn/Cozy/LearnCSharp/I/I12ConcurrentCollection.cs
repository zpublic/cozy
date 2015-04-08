using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I12ConcurrentCollection
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //并发集合
            //ConcurrentXXX这些集合都是线程安全的,实现了IProducerConsumerCollection<T>接口
            //里面如下的方法

            //[__DynamicallyInvokable]
            //void CopyTo(T[] array, int index);
        
            //[__DynamicallyInvokable]
            //bool TryAdd(T item);
        
            //[__DynamicallyInvokable]
            //bool TryTake(out T item);
        
            //[__DynamicallyInvokable]
            //T[] ToArray();

            //TryXX()方法返回一个bool值，说明操作是否成功

            //ConcurrentQueue<T>
            var concurrentQueue = new ConcurrentQueue<int>();
            concurrentQueue.Enqueue(1);
            int i;
            var reslut = concurrentQueue.TryPeek(out i);
            reslut = concurrentQueue.TryDequeue(out i);

            //ConcurrentStack<T>
            var concurrentStack = new ConcurrentStack<int>();
            concurrentStack.Push(1);
            reslut = concurrentStack.TryPeek(out i);
            reslut = concurrentStack.TryPop(out i);

            //ConcurrentBag<T>
            var concurrentBag = new ConcurrentBag<int>();
            concurrentBag.Add(1);
            reslut = concurrentBag.TryPeek(out i);
            reslut = concurrentBag.TryTake(out i);

            //ConcurrentDictionary<TKey,TValue>
            //该集合没实现IProducerConsumerCollection<T>，因此它的TryXX()是以非堵塞的方式访问成员
            var concurrentDictionary = new ConcurrentDictionary<int, int>();
            reslut = concurrentDictionary.TryAdd(1, 1);
            reslut = concurrentDictionary.TryRemove(1, out i);

            //BlockingCollection<T>,该集合的Add()和Taake()会阻塞线程并且一直等待
            var blockingCollection = new BlockingCollection<int>();

            var events  = new ManualResetEventSlim[2];
            var waits = new WaitHandle[2];

            for (int j = 0; j < events.Length; j++)
            {
                events[j] = new ManualResetEventSlim(false);
                waits[j] = events[j].WaitHandle;
            }

            new Thread(() =>
            {
                for (int j = 0; j < 300; j++)
                {
                    blockingCollection.Add(j);
                }
                events[0].Set();
            }).Start();

            new Thread(() =>
            {
                for (int j = 0; j < 300; j++)
                {
                    blockingCollection.Take();
                }
                events[1].Set();
            }).Start();

            if (!WaitHandle.WaitAll(waits))
            {
                Console.WriteLine("wait failed");
            }
            else
            {
                Console.WriteLine("reading/writing finished");
            }
        }
    }
}
