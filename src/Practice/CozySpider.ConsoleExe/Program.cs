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
using CozySpider.Core.UrlFilter;
using CozySpider.Core.Reader;

namespace CozySpider.ConsoleExe
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Init");
            InitRecvThread();

            SpiderSeeds seeds = new SpiderSeeds();
            for(int i = 0; i <= 10; ++i)
            {
                seeds.AddSeed("http://www.cozy.com/cn/actresses/currentPage/" + i);
            }

            IUrlMatch match = new FindStringMatch()
            {
                StringFind  = "www.cozy.com/cn/star/",
                NoCase      = true
            };

            IUrlFilter filter = new BloomFilter();

            IUrlReader reader = new DefaultReader();

            SpiderSetting setting = new SpiderSetting();
            setting.Depth           = 1;
            setting.WorkerCount     = 8;
            setting.Seeds           = seeds;
            setting.Match           = match;
            setting.Filter          = filter;
            setting.Reader          = reader;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.AddUrlEventHandler       += OnEvent;
            master.DataReceivedEventHandler += OnEvent;
            master.ErrorEventHandler        += OnEvent;

            Console.WriteLine("Begin");
            master.Crawl();

            master.Stop();
            StopRecvThread();
            Console.WriteLine("search Finish");

            var parsetask = new Task(Parse, 5);
            parsetask.Start();
            parsetask.Wait();

            Console.WriteLine("Parse Finish");
            foreach (var obj in InfoList)
            {
                Console.WriteLine(obj.ToString());
            }
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
                while(true)
                {
                    if(MessageQueue.Count == 0)
                    {
                        RecvEvent.WaitOne();
                    }

                    lock (locker)
                    {
                        if(MessageQueue.Count > 0)
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
            lock(locker)
            {
                MessageQueue.Enqueue(e.Url);
                RecvEvent.Set();
            }
        }
        private static List<StarInfo> InfoList = new List<StarInfo>();
        private static object listLocker = new object();

        private static void Parse(object param)
        {
            while (true)
            {
                string result = null;
                lock (chacheLocker)
                {
                    if(WebCache.Count > 0)
                    {
                        result = WebCache.Dequeue();
                    }
                }
                if (result != null)
                {
                    try
                    {
                        var Info = new StarInfo();
                        var content = reader.Read(result);
                        var boxpos = content.IndexOf("<div class=\"avatar-box\">");
                        if (boxpos != -1)
                        {
                            var photopos = content.IndexOf("<div class=\"photo-frame\">", boxpos);
                            if (photopos != -1)
                            {
                                var photo = MatchText(@"http://", "\"", content, photopos);
                                if(photo != null)
                                {
                                   Info.Photo = photo;
                                }
                            }
                            var infopos = content.IndexOf("<div class=\"photo-info\">", photopos);
                            if (infopos != -1)
                            {
                                var name = MatchText("<span class=\"pb10\">", @"</span>", content, infopos);
                                if(name != null)
                                {
                                    Info.Name = name;

                                }
                                var birthday = MatchText("<p>生日:", @"</p>", content, infopos);
                                if(birthday != null)
                                {
                                    Info.Birthday = birthday;
                                }

                                var age = MatchText("<p>年龄:", @"</p>", content, infopos);
                                if (age != null)
                                {
                                    Info.Age = age;
                                }

                                var cup = MatchText("<p>罩杯:", @"</p>", content, infopos);
                                if (cup != null)
                                {
                                    Info.Cup = cup;
                                }

                                var chest = MatchText("<p>胸围:", @"</p>", content, infopos);
                                if (chest != null)
                                {
                                    Info.Chest = chest;
                                }

                                var waist = MatchText("<p>腰围:", @"</p>", content, infopos);
                                if (waist != null)
                                {
                                    Info.Waist = waist;
                                }

                                var hip = MatchText("<p>臀围:", @"</p>", content, infopos);
                                if (hip != null)
                                {
                                    Info.Hip = hip;
                                }
                            }
                        }
                        lock(listLocker)
                        {
                            InfoList.Add(Info);
                        }
                        Console.WriteLine("获取到一个 还剩{0}个", WebCache.Count);
                    }
                    catch(Exception)
                    {

                    }
                }
                else
                {
                    break;
                }
            }
        }

        private static string MatchText(string first, string end, string text, int beginpos)
        {
            var pos = text.IndexOf(first, beginpos);
            if (pos != -1)
            {
                pos += first.Length;
                return text.Substring(pos, text.IndexOf(end, pos) - pos);
            }
            return null;
        }
    }
}
