using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.L
{
    class L1AsynchronousPatterns
    {
        static string url = "http://apistore.baidu.com/microservice/iplookup?ip=113.106.106.131";
        static CancellationTokenSource cts = new CancellationTokenSource();

        static void SyncGet()
        {
            WebClient client = new WebClient();
            string resp = client.DownloadString(url);
            Console.WriteLine(resp);
        }

        static void AsyncGet()
        {
            Func<string, string> downloadString = (address) =>
            {
                var client = new WebClient();
                return client.DownloadString(address);
            };

//             downloadString.BeginInvoke(url, ar =>
//             {
//                 string resp = downloadString.EndInvoke(ar);
//                 Console.WriteLine(resp);
//             }, null);

            var ar = downloadString.BeginInvoke(url, null, null);
            string resp = downloadString.EndInvoke(ar);
            Console.WriteLine(resp);
        }

        static void AsyncEventGet()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender1, e1) =>
            {
                string resp = e1.Result;
                Console.WriteLine(resp);
            };
            client.DownloadStringAsync(new Uri(url));
        }

        static async void AsyncTaskGet()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url, cts.Token);
                string resp = await response.Content.ReadAsStringAsync();

                await Task.Run(() =>
                {
                    Console.WriteLine(resp);
                }, cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //SyncGet();
            //AsyncGet();
            //AsyncEventGet();
            AsyncTaskGet();
            cts.Cancel();
        }
    }
}
