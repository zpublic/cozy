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
        private static AutoResetEvent CompleteEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Init");

            SpiderSeeds seeds = new SpiderSeeds();
            seeds.AddSeed("http://www.javfee.com/cn/genre/3t");
            IUrlMatch match = new FindStringMatch()
            {
                StringFind  = ".jpg",
                NoCase      = true
            };

            SpiderSetting setting = new SpiderSetting();
            setting.Depth           = 2;
            setting.WorkerCount     = 2;
            setting.Seeds           = seeds;
            setting.Match           = match;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.AddUrlEventHandler       += OnAddUrlEvent;
            master.DataReceivedEventHandler += OnDataReceivedEvent;

            Console.WriteLine("Begin");
            master.Crawl();

            CompleteEvent.WaitOne();
            master.Stop();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        private static int id;
        public static int Id { get { return id++; } }

        private static void OnAddUrlEvent(object sender, Core.Event.AddUrlEventArgs args)
        {
            Console.WriteLine(args.Url);
            WebRequest request  = WebRequest.Create(args.Url.Trim());
            WebResponse respone = request.GetResponse();
            Stream rspStream    = respone.GetResponseStream();

            string path         = @"./img/";

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fs = new FileStream(path + Id + ".jpg", FileMode.Create, FileAccess.ReadWrite))
            {
                rspStream.CopyTo(fs);
            }
        }

        private static void OnDataReceivedEvent(object sender, Core.Event.DataReceivedEventArgs args)
        {
            CompleteEvent.Set();
        }
    }
}
