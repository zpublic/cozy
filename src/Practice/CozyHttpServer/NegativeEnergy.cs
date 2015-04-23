using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyHttpServer
{
    public class NegativeEnergy : NancyModule
    {
        public static string[] strline = null;
        public static void LoadData()
        {
            string fileName = System.Environment.CurrentDirectory + "/NegativeEnergy.txt";
            string text = File.ReadAllText(fileName, Encoding.ASCII);
            strline = File.ReadAllLines(fileName);
        }

        private Random r = new Random();
        public NegativeEnergy()
        {
            Get["/fnl"] = x =>
            {
                string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>每天来点负能量</title></head><body>"
                    + strline.ElementAt(r.Next(strline.Count() - 1))
                    + "</body></html>";

//                 Encoding gb2312 = Encoding.GetEncoding("gb2312");
//                 byte[] b = Encoding.Default.GetBytes(strline.ElementAt(r.Next(strline.Count() - 1)));
//                 string gb2312str = gb2312.GetString(b);
                return html;
            };
        }
    }
}
