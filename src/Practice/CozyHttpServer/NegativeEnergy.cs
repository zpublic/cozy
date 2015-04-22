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
                return strline.ElementAt(r.Next(strline.Count() - 1));
            };
        }
    }
}
