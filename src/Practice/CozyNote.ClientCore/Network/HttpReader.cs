using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace CozyNote.ClientCore.Network
{
    public static class HttpReader
    {
        public const int MaxBufferSize  = 256000;

        public const string UA          = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

        private static HttpClient GetDefaultClient()
        {
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = MaxBufferSize;
            client.DefaultRequestHeaders.Add("user-agent", UA);
            return client;
        }

        public static string HttpGet(string url)
        {
            var client      = GetDefaultClient();
            var response    = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string HttpPost(string url, string data)
        {
            var client      = GetDefaultClient();

            var content     = new StringContent(data);
            var response    = client.PostAsync(new Uri(url), content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
