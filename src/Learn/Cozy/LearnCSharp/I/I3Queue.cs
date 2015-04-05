using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I3Queue
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //队列在按接收顺序存储消息方面非常有用，以便于进行顺序处理。 此类将队列作为循环数组实现。 存储在 Queue 中的对象在一端插入，从另一端移除。
            //Queue 的容量是 Queue 可以包含的元素数。 随着向 Queue 中添加元素，容量通过重新分配按需自动增加。可通过调用 TrimToSize 来减少容量。
            //等比因子是当需要更大容量时当前容量要乘以的数字。在构造 Queue 时确定增长因子。 默认增长因子为 2.0。 Queue 的容量将始终加 4，
            //无论增长因子是多少。 例如，当需要更大的容量时，增长因子为 1.0 的 Queue 的容量将始终增加四倍。
            //Queue 接受 null 作为有效值并且允许重复的元素。
            //摘自MSDN https://msdn.microsoft.com/zh-cn/library/system.collections.queue.aspx

            //创建一个队列
            var queue = new Queue<int>();

            //在队列尾部添加一个元素
            queue.Enqueue(1);
            queue.Enqueue(2);

            //取出(访问)队列中的元素(因为Queue<T>不实现IList<T>接口，所有不能使用索引执行访问)
            queue.Dequeue();

            //读取但不移除
            queue.Peek();
        }
    }
}
