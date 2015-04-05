using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.L
{
    class L2AsynchronousProgramming
    {
        private static string Greeting(string name)
        {
            Console.WriteLine("running greeting in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            return string.Format("Hello, {0}", name);
        }

        private static Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() =>
            {
                Console.WriteLine("running greetingasync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return Greeting(name);
            });
        }

        private static Func<string, string> greetingInvoker = Greeting;

        static IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
        {
            return greetingInvoker.BeginInvoke(name, callback, state);
        }

        static string EndGreeting(IAsyncResult ar)
        {
            return greetingInvoker.EndInvoke(ar);
        }

        private async static void CallerWithAsync()
        {
            Console.WriteLine("started CallerWithAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            string result = await GreetingAsync("Stephanie");
            Console.WriteLine(result);
            Console.WriteLine("finished GreetingAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
        }

        private async static void CallerWithAsync2()
        {
            Console.WriteLine("started CallerWithAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Console.WriteLine(await GreetingAsync("Stephanie"));
            Console.WriteLine("finished GreetingAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
        }

        private static void CallerWithContinuationTask()
        {
            Console.WriteLine("started CallerWithContinuationTask in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            var t1 = GreetingAsync("Stephanie");
            t1.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);
                Console.WriteLine("finished CallerWithContinuationTask in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            });
        }

        private static void CallerWithAwaiter()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            string result = GreetingAsync("Matthias").GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private async static void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", s1, s2);
        }

        private async static void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            await Task.WhenAll(t1, t2);
            Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", t1.Result, t2.Result);
        }

        private async static void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            string[] result = await Task.WhenAll(t1, t2);
            Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", result[0], result[1]);
        }

        private static async void ConvertingAsyncPattern()
        {
            string r = await Task<string>.Factory.FromAsync<string>(BeginGreeting, EndGreeting, "Angela", null);
            Console.WriteLine(r);
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            CallerWithAsync();
            CallerWithAsync2();
            //CallerWithContinuationTask();
            CallerWithAwaiter();
            //MultipleAsyncMethods();
            //MultipleAsyncMethodsWithCombinators1();
            //MultipleAsyncMethodsWithCombinators2();
            //ConvertingAsyncPattern();
        }
    }
}
