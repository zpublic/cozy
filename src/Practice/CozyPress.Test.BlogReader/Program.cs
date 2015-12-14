using CozyInfrastructure.HttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CozyPress.Test.BlogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = "http://localhost:12306/";
            var url = host + "/blog/get";

            var v = new Dictionary<string, string>();
            v.Add("page", "1");
            HttpContent http_content = new FormUrlEncodedContent(v);

            var rsp = HttpPost.Post(host, url, http_content);
            string rspText = rsp.Content.ReadAsStringAsync().Result;
            Console.WriteLine(rspText);
        }
    }
}
