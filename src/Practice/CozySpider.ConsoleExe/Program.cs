using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core;
using CozySpider.Core.UrlMatch;
using System.Threading;
using CozySpider.Core.Event;
using System.IO;
using System.Net;

namespace CozySpider.ConsoleExe
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Init");
            InitRecvThread();

            SpiderSeeds seeds = new SpiderSeeds();
            seeds.AddSeed("http://www.javfee.com/cn/genre/3t");
            IUrlMatch match = new FindStringMatch()
            {
                StringFind  = "www.javfee.com",
                NoCase      = true
            };

            SpiderSetting setting = new SpiderSetting();
            setting.Depth           = 2;
            setting.WorkerCount     = 2;
            setting.Seeds           = seeds;
            setting.Match           = match;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.AddUrlEventHandler       += OnEvent;
            master.DataReceivedEventHandler += OnEvent;
            master.ErrorEventHandler        += OnEvent;

            Console.WriteLine("Begin");
            master.Crawl();

            Console.ReadKey();
            master.Stop();
            StopRecvThread();
            Console.WriteLine("Finish");
        }

        static AutoResetEvent ARE = new AutoResetEvent(false);

        static Queue<string> MessageQueue = new Queue<string>();
        static object locker = new object();

        static Thread RecvThread { get; set; }

        private static void InitRecvThread()
        {
            var RecvThread = new Thread(new ThreadStart(() => 
            {
                while(true)
                {
                    if(MessageQueue.Count == 0)
                    {
                        ARE.WaitOne();
                    }

                    lock (locker)
                    {
                        if(MessageQueue.Count > 0)
                        {
                            var result = MessageQueue.Dequeue();
                            Console.WriteLine(result);
                        }
                    }
                    Thread.Sleep(0);
                }
            }));
            RecvThread.Start();
        }

        private static void StopRecvThread()
        {
            RecvThread.Abort();
        }

        private static void OnEvent(object sender, EventArgsBase e)
        {
            lock(locker)
            {
                MessageQueue.Enqueue(e.Message);
                ARE.Set();
            }
        }
    }
}
