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
        private static string[] strline1 = null;
        private static string[] strline2 = null;
        public static void LoadData()
        {
            string fileName = System.Environment.CurrentDirectory + "/NegativeEnergy.txt";
            string text = File.ReadAllText(fileName, Encoding.ASCII);
            strline1 = File.ReadAllLines(fileName);

            string fileName2 = System.Environment.CurrentDirectory + "/PositiveEnergy.txt";
            string text2 = File.ReadAllText(fileName2, Encoding.ASCII);
            strline2 = File.ReadAllLines(fileName2);
        }

        private Random r = new Random();
        public NegativeEnergy()
        {
            Get["/fnl"] = x =>
            {
                string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>每天来点负能量</title></head><body>"
                    + strline1.ElementAt(r.Next(strline1.Count() - 1))
                    + "</body></html>";
                return html;
            };
            Get["/znl"] = x =>
            {
                string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>每天来点负能量</title></head><body>"
                    + strline2.ElementAt(r.Next(strline2.Count() - 1))
                    + "</body></html>";
                return html;
            };
        }
    }
}
