using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.L
{
    class L4Cancellation
    {
        static string url = "http://apistore.baidu.com/microservice/iplookup?ip=113.106.106.131";
        private static CancellationTokenSource cts;

        static async void AsyncTaskGet1()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url, cts.Token);
                string resp = await response.Content.ReadAsStringAsync();
                Console.WriteLine(resp);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void AsyncTaskGet2()
        {
            await Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i < 1000; ++i)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        Task.Delay(100);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }, cts.Token);
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

//             cts = new CancellationTokenSource();
//             AsyncTaskGet1();
//             cts.Cancel();
            cts = new CancellationTokenSource();
            AsyncTaskGet2();
            cts.Cancel();
        }
    }
}
