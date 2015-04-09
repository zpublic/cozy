using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnFoundation.A
{
    class A3CancellationFramework
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            CancelParallelLoop();
            CancelTask();
        }

        static void CancelParallelLoop()
        {
            var cts = new CancellationTokenSource();
            cts.Token.ThrowIfCancellationRequested();
            cts.Token.Register(() => Console.WriteLine("** token cancelled"));

            // start a task that sends a cancel after 500 ms      
            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult result =
                   Parallel.For(0, 100,
                       new ParallelOptions()
                       {
                           CancellationToken = cts.Token
                       },
                       x =>
                       {
                           Console.WriteLine("loop {0} started", x);
                           int sum = 0;
                           for (int i = 0; i < 100; i++)
                           {
                               Thread.Sleep(2);
                               sum += i;
                           }
                           Console.WriteLine("loop {0} finished", x);
                       });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** task cancelled"));

            // send a cancel after 500 ms
            cts.CancelAfter(500);

            Task t1 = Task.Run(() =>
            {
                Console.WriteLine("in task");
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("cancelling was requested, cancelling from within the task");
                        try
                        {
                            token.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                    Console.WriteLine("in loop");
                }
                Console.WriteLine("task finished without cancellation");
            }, cts.Token);

            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("exception: {0}, {1}", ex.GetType().Name, ex.Message);
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine("inner excepion: {0}, {1}", ex.InnerException.GetType().Name, ex.InnerException.Message);
                }
            }
        }
    }
}
