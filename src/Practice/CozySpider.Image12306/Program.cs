using CozySpider.Core;
using CozySpider.Core.Event;
using CozySpider.Core.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozySpider.Image12306
{
    class Program
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Init");
            InitRecvThread();

            SpiderSeeds seeds = new SpiderSeeds();
            for (int i = 0; i <= 10; ++i)
            {
                seeds.AddSeed("https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&" + r.NextDouble().ToString().Substring(0, 10));
            }

            /*IUrlMatch match = new FindStringMatch()
            {
                StringFind = "www.cozy.com/cn/star/",
                NoCase = true
            };*/

            //IUrlFilter filter = new BloomFilter();

            IUrlReader reader = new DefaultReader();

            SpiderSetting setting = new SpiderSetting();
            setting.Depth = 1;
            setting.WorkerCount = 8;
            setting.Seeds = seeds;
            //setting.Match = match;
            //setting.Filter = filter;
            setting.Reader = reader;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.AddUrlEventHandler += OnEvent;
            master.DataReceivedEventHandler += OnEvent;
            master.ErrorEventHandler += OnEvent;

            Console.WriteLine("Begin");
            master.Crawl();

            master.Stop();
            StopRecvThread();
            Console.WriteLine("search Finish");
            Console.ReadKey();
        }

        static AutoResetEvent RecvEvent = new AutoResetEvent(false);

        static Queue<string> MessageQueue = new Queue<string>();
        static object locker = new object();

        static Queue<string> WebCache = new Queue<string>();
        static object chacheLocker = new object();

        static Thread RecvThread { get; set; }

        static DefaultReader reader = new DefaultReader();

        private static void InitRecvThread()
        {
            RecvThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    if (MessageQueue.Count == 0)
                    {
                        RecvEvent.WaitOne();
                    }

                    lock (locker)
                    {
                        if (MessageQueue.Count > 0)
                        {
                            var result = MessageQueue.Dequeue();
                            WebCache.Enqueue(result);
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
            lock (locker)
            {
                MessageQueue.Enqueue(e.Url);
                RecvEvent.Set();
            }
        }
    }
}
