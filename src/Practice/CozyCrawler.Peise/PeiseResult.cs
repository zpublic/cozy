using CozyCrawler.Base;
using CozyCrawler.Interface;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CozyCrawler.Peise
{
    class PeiseResult : IUrl2Result
    {
        private List<Palette> Data { get; set; }

        public PeiseResult(List<Palette> data)
        {
            Data = data;
        }

        public void OnNewUrl(string url)
        {
            var obj = JsonConvert.DeserializeObject<Palette>(url);
            Data.Add(obj);

            lock (this)
            {
                Console.WriteLine(obj.Name);
                foreach(var rgb in obj.RGB)
                {
                    Console.WriteLine(rgb);
                }
                Console.WriteLine("------------------------");
            }
        }
    }
}
