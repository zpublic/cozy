using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnFoundation.A
{
    class A1Parallel
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            ParallelFor();
            ParallelForeach();
            ParallelInvoke();
        }

        static void ParallelFor()
        {
            // simple scenario
            ParallelLoopResult result =
                Parallel.For(0, 5, i =>
                {
                    Console.WriteLine("{0}, task: {1}, thread: {2}", i,
                       Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(10);
                });
            Console.WriteLine("is completed: {0}", result.IsCompleted);
            //Console.ReadKey();

            ParallelLoopResult result1 =
                Parallel.For(0, 5, async i =>
                {
                    Console.WriteLine("{0}, task: {1}, thread: {2}", i,
                       Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                    await Task.Delay(10);
                    Console.WriteLine("{0}, task: {1}, thread: {2}", i,
                      Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                });
            Console.WriteLine("is completed: {0}", result1.IsCompleted);
            //Console.ReadKey();

            //breaking early
            ParallelLoopResult result2 =
                Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
                {
                    Console.WriteLine("i: {0} task {1}", i, Task.CurrentId);
                    Thread.Sleep(10);
                    if (i > 15)
                        pls.Break();
                });
            Console.WriteLine("Is completed: {0}", result2.IsCompleted);
            if (!result2.IsCompleted)
                Console.WriteLine("lowest break iteration: {0}", result2.LowestBreakIteration);

            Parallel.For<string>(0, 20, () =>
            {
                // invoked once for each thread
                Console.WriteLine("init thread {0}, task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return String.Format("t{0}", Thread.CurrentThread.ManagedThreadId);
            },
            (i, pls, str1) =>
            {
                // invoked for each member
                Console.WriteLine("body i {0} str1 {1} thread {2} task {3}", i, str1,
                    Thread.CurrentThread.ManagedThreadId,
                    Task.CurrentId);
                Thread.Sleep(10);
                return String.Format("i {0}", i);
            },
            (str1) =>
            {
                // final action on each thread
                Console.WriteLine("finally {0}", str1);
            });
        }

        static void ParallelForeach()
        {
            string[] data = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };

            ParallelLoopResult result =
                Parallel.ForEach<string>(data, s =>
                {
                    Console.WriteLine(s);
                });
            Parallel.ForEach<string>(data,
                (s, pls, l) =>
                {
                    Console.WriteLine("{0} {1}", s, l);
                });
        }

        static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }

        static void Foo()
        {
            Console.WriteLine("foo");
        }

        static void Bar()
        {
            Console.WriteLine("bar");
        }
    }
}
