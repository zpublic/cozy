using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CozyHttpServer
{
    public class NegativeEnergy : NancyModule
    {
        private static string[] strline = null;
        public static void LoadData()
        {
            string fileName = System.Environment.CurrentDirectory + "/NegativeEnergy.txt";
            string text = File.ReadAllText(fileName, Encoding.ASCII);
            strline = File.ReadAllLines(fileName);
        }

        private static string GetHtml(string text)
        {
            string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>每天来点负能量</title></head><body>"
                    + text
                    + "</body></html>";
            return html;
        }
        private static string OperateSuccess = GetHtml("操作成功<br /><a href=\"http://www.laorouji.com:48360/fnl\">再来一条</a>");

        private NegativeEnergyDb db = new NegativeEnergyDb();
        private Random r = new Random();
        public NegativeEnergy()
        {
            Get["/fnl"] = x =>
            {
                int index = r.Next(strline.Count() - 1);
                string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>每天来点负能量</title></head><body>"
                    + strline.ElementAt(index)
                    + "<br />被赞次数："
                    + db.GetZanNum(index.ToString()).ToString()
                    + "<br /><a href=\"http://www.laorouji.com:48360/fnl/"
                    + index.ToString()
                    + "\">赞一下</a><br /><a href=\"http://www.laorouji.com:48360/fnl\">再来一条</a></body></html>";
                return html;
            };
            Get["/fnl/{name}"] = x =>
            {
                db.AddZanNum(x.name, 1);
                return OperateSuccess;
            };
        }
    }
}
