using CozySpider.Core;
using CozySpider.Core.Event;
using CozySpider.Core.Reader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

            IUrlReader reader = new ImageReader();

            SpiderSetting setting = new SpiderSetting();
            setting.Depth = 1;
            setting.WorkerCount = 8;
            setting.Seeds = seeds;
            setting.Reader = reader;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.DataReceivedEventHandler += OnDataEvent;

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

        private static MD5 _Md5 { get; set; } = new MD5CryptoServiceProvider();

        private static void OnDataEvent(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data.Length);
            var md5Byte = _Md5.ComputeHash(e.Data);

            var sb = new StringBuilder();
            for(int i = 0; i < md5Byte.Length; ++i)
            {
                sb.Append(md5Byte[i].ToString("x2"));
            }

            var fileName = sb.ToString();

            using (var ms = new MemoryStream(e.Data))
            {
                Image.FromStream(ms).Save(fileName + ".jpg");
            }
        }
    }
}
