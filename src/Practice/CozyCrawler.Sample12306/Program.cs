using CozyCrawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Sample12306
{
    class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            var p1 = new BlockedGenerateUrlRunner();
            var p2 = new BlockedUrl2ResultRunner();

            var urls = new Core.UrlGenerater.FixedUrls();
            for (int i = 0; i <= 3; ++i)
            {
                urls.Urls.Add("https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&" + r.NextDouble().ToString().Substring(0, 10));
            }

            var downloader = new Core.Url2Result.JpgDownloader();
            downloader.SetSavePath(@"g:\res\");

            p1.From(urls);
            p1.To(p2);
            p2.To(downloader);

            p2.Start();
            p1.Start();
        }
    }
}
