using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnFoundation.A
{
    class A7Synchronization
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < 5; i++)
            {
                SyncSample();
            }

            SemaphoreMain();

            EventMain();

            BarrierMain();

            RWMain();
        }

        private static void SemaphoreMain()
        {
            int taskCount = 6;
            int semaphoreCount = 3;
            var semaphore = new SemaphoreSlim(semaphoreCount, semaphoreCount);
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() => SemaphoreTaskMain(semaphore));
            }
            Task.WaitAll(tasks);
            Console.WriteLine("All tasks finished");
        }

        static void SyncSample()
        {
            object obj = new object();
            bool lockTaken = false;
            Monitor.TryEnter(obj, 500, ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    // acquired the lock
                    // synchronized region for obj
                }
                finally
                {
                    Monitor.Exit(obj);
                }

            }
            else
            {
                // didn't get the lock, do something else
            }

            int numTasks = 20;
            var state = new SharedState();
            var tasks = new Task[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i].Wait();
            }

            Console.WriteLine("summarized {0}", state.State);
        }

        static void SemaphoreTaskMain(SemaphoreSlim semaphore)
        {
            bool isCompleted = false;
            while (!isCompleted)
            {
                if (semaphore.Wait(100))
                {
                    try
                    {
                        Console.WriteLine("Task {0} locks the semaphore", Task.CurrentId);
                        Thread.Sleep(200);
                    }
                    finally
                    {
                        Console.WriteLine("Task {0} releases the semaphore", Task.CurrentId);
                        semaphore.Release();
                        isCompleted = true;
                    }
                }
                else
                {
                    Console.WriteLine("Timeout for task {0}; wait again",
                       Task.CurrentId);
                }
            }
        }

        private static void EventMain()
        {
            const int taskCount2 = 4;

            var mEvents = new ManualResetEventSlim[taskCount2];
            // var cEvent = new CountdownEvent(taskCount);

            var waitHandles = new WaitHandle[taskCount2];
            var calcs = new Calculator[taskCount2];

            for (int i = 0; i < taskCount2; i++)
            {
                int i1 = i;
                mEvents[i] = new ManualResetEventSlim(false);
                waitHandles[i] = mEvents[i].WaitHandle;
                calcs[i] = new Calculator(mEvents[i]);
                //calcs[i] = new Calculator(cEvent);

                Task.Run(() => calcs[i1].Calculation(i1 + 1, i1 + 3));

            }

            //cEvent.Wait();
            //Console.WriteLine("all finished");
            //for (int i = 0; i < taskCount; i++)
            //{
            //    Console.WriteLine("task for {0}, result: {1}", i, calcs[i].Result);
            //}

            for (int i = 0; i < taskCount2; i++)
            {
                int index = WaitHandle.WaitAny(waitHandles);
                if (index == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("Timeout!!");
                }
                else
                {
                    mEvents[index].Reset();
                    Console.WriteLine("finished task for {0}, result: {1}",
                                      index, calcs[index].Result);
                }
            }
        }

        static void BarrierMain()
        {
            const int numberTasks = 2;
            const int partitionSize = 1000000;
            var data = new List<string>(FillData(partitionSize * numberTasks));

            var barrier = new Barrier(numberTasks + 1);

            var tasks = new Task<int[]>[numberTasks];
            for (int i = 0; i < numberTasks; i++)
            {
                //tasks[i] = taskFactory.StartNew<int[]>(CalculationInTask,
                //    Tuple.Create(i, partitionSize, barrier, data));
                int jobNumber = i;
                tasks[i] = Task.Run(() => CalculationInTask(jobNumber, partitionSize, barrier, data));
            }

            barrier.SignalAndWait();
            var resultCollection = tasks[0].Result.Zip(tasks[1].Result, (c1, c2) =>
            {
                return c1 + c2;
            });

            char ch = 'a';
            int sum = 0;
            foreach (var x in resultCollection)
            {
                Console.WriteLine("{0}, count: {1}", ch++, x);
                sum += x;
            }

            Console.WriteLine("main finished {0}", sum);
            Console.WriteLine("remaining {0}, phase {1}", barrier.ParticipantsRemaining, barrier.CurrentPhaseNumber);

        }

        //        static int[] CalculationInTask(object p)
        static int[] CalculationInTask(int jobNumber, int partitionSize, Barrier barrier, IList<string> coll)
        {
            var data = new List<string>(coll);
            int start = jobNumber * partitionSize;
            int end = start + partitionSize;
            Console.WriteLine("Task {0}: partition from {1} to {2}", Task.CurrentId, start, end);
            int[] charCount = new int[26];
            for (int j = start; j < end; j++)
            {
                char c = data[j][0];
                charCount[c - 97]++;
            }
            Console.WriteLine("Calculation completed from task {0}. {1} times a, {2} times z", Task.CurrentId, charCount[0], charCount[25]);

            barrier.RemoveParticipant();
            Console.WriteLine("Task {0} removed from barrier, remaining participants {1}", Task.CurrentId, barrier.ParticipantsRemaining);
            return charCount;
        }

        public static IEnumerable<string> FillData(int size)
        {
            var data = new List<string>(size);
            var r = new Random();
            for (int i = 0; i < size; i++)
            {
                data.Add(GetString(r));
            }
            return data;
        }
        private static string GetString(Random r)
        {
            var sb = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                sb.Append((char)(r.Next(26) + 97));
            }
            return sb.ToString();
        }

        private static List<int> items = new List<int>() { 0, 1, 2, 3, 4, 5 };
        private static ReaderWriterLockSlim rwl = new
              ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        static void ReaderMethod(object reader)
        {
            try
            {
                rwl.EnterReadLock();

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine("reader {0}, loop: {1}, item: {2}",
                          reader, i, items[i]);
                    Thread.Sleep(40);
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        static void WriterMethod(object writer)
        {
            try
            {
                while (!rwl.TryEnterWriteLock(50))
                {
                    Console.WriteLine("Writer {0} waiting for the write lock",
                          writer);
                    Console.WriteLine("current reader count: {0}",
                          rwl.CurrentReadCount);
                }
                Console.WriteLine("Writer {0} acquired the lock", writer);
                for (int i = 0; i < items.Count; i++)
                {
                    items[i]++;
                    Thread.Sleep(50);
                }
                Console.WriteLine("Writer {0} finished", writer);
            }
            finally
            {
                rwl.ExitWriteLock();
            }
        }

        static void RWMain()
        {
            var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            var tasks = new Task[6];
            tasks[0] = taskFactory.StartNew(WriterMethod, 1);
            tasks[1] = taskFactory.StartNew(ReaderMethod, 1);
            tasks[2] = taskFactory.StartNew(ReaderMethod, 2);
            tasks[3] = taskFactory.StartNew(WriterMethod, 2);
            tasks[4] = taskFactory.StartNew(ReaderMethod, 3);
            tasks[5] = taskFactory.StartNew(ReaderMethod, 4);

            for (int i = 0; i < 6; i++)
            {
                tasks[i].Wait();
            }
        }
    }
}
