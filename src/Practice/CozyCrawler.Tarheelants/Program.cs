using CozyCrawler.Component.Url2Result;
using CozyCrawler.Component.Url2Url;
using CozyCrawler.Component.UrlGenerater;
using CozyCrawler.Interface;
using CozyCrawler.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Tarheelants
{
    class Program
    {
        static void Main(string[] args)
        {
            IUrlGeneraterRunner p1 = new MultiUrlGeneraterRunner();
            IUrl2UrlRunner p2 = new BlockedUrl2UrlRunner();
            IUrl2UrlRunner p3 = new BlockedUrl2UrlRunner();
            IUrl2UrlRunner p4 = new BlockedUrl2UrlRunner();

            var urls = new FixedUrls();
            urls.Urls.Add("https://tarheelants.com/collections");
            p1.From(urls);

            p2.SetProcessor(new Main2Collects(4));
            p1.To(p2);

            p3.SetProcessor(new Collect2Items(4));
            p2.To(p3);

            p4.SetProcessor(new Item2Image(4));
            p3.To(p4);

            var downloader = new XJpgDownloader();
            downloader.SetSavePath(@"F:\ant\tarheelants\");
            p4.To(downloader);


            p4.Start();
            p3.Start();
            p2.Start();
            p1.Start();
            Console.ReadKey();
        }
    }
}
