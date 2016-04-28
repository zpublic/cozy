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
    class UserColorResult : IUrl2Result
    {
        private List<UserColor> Data { get; set; }

        public UserColorResult(List<UserColor> data)
        {
            Data = data;
        }

        public void OnNewUrl(string url)
        {
            var obj = JsonConvert.DeserializeObject<UserColor>(url);
            Data.Add(obj);

            lock (this)
            {
                Console.WriteLine(obj.Name);
                Console.WriteLine(obj.RGB);
                Console.WriteLine("------------------------");
            }
        }
    }
}
