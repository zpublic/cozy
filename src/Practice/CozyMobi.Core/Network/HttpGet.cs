using CozyMobi.Core.Model;
using CozyMobi.Core.RequestBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CozyMobi.Core.Network
{
    class HttpGet
    {
        public static HttpResponseMessage Get(string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseCookies = false };
            HttpClient client = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = client.SendAsync(request).Result;
            return response;
        }
    }
}
