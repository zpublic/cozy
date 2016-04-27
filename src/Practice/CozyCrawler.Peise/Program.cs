using CozyCrawler.Interface;
using CozyCrawler.Runner;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Peise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OnPalette();
            OnColor();
        }

        private static void OnPalette()
        {
            IUrlGeneraterRunner p1 = new MultiUrlGeneraterRunner();
            IUrl2UrlRunner p2 = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner p3 = new AsyncUrl2ResultRunner();

            var result = new List<Palette>();

            p1.From(new PeiseUrlGenerater(1, 132));
            p1.To(p2);
            p2.To(p3);
            p2.SetProcessor(new PeiseUrl2Url(4));
            p3.To(new PeiseResult(result));

            p3.Start();
            p2.Start();
            p1.Start();

            Console.ReadKey();
            using (var fs = new FileStream("Palette.json", FileMode.Create, FileAccess.ReadWrite))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(JsonConvert.SerializeObject(result));
                }
            }
        }

        private static void OnColor()
        {
            IUrlGeneraterRunner p1 = new MultiUrlGeneraterRunner();
            IUrl2UrlRunner p2 = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner p3 = new AsyncUrl2ResultRunner();

            var result = new List<UserColor>();

            p1.From(new UserColorUrlGenerater(1, 132));
            p1.To(p2);
            p2.To(p3);
            p2.SetProcessor(new UserColorUrl2Url(4));
            p3.To(new UserColorResult(result));

            p3.Start();
            p2.Start();
            p1.Start();

            Console.ReadKey();
            using (var fs = new FileStream("Color.json", FileMode.Create, FileAccess.ReadWrite))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(JsonConvert.SerializeObject(result));
                }
            }
        }
    }
}
