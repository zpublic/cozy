using System;
using System.Net;
using System.Net.Http;

namespace CozyInfrastructure.HttpClient
{
    class HttpGet
    {
        public static HttpResponseMessage Get(string host, string url)
        {
            Uri uri = new Uri(host);
            HttpClientHandler handler = new HttpClientHandler { UseCookies = true };
            CookieContainer CookieContainer = HttpCookie.GetUriCookieContainer(uri);
            if (CookieContainer != null)
            {
                handler.CookieContainer = CookieContainer;
            }
            else
            {
                handler.CookieContainer = new CookieContainer();
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);
            HttpResponseMessage response = client.SendAsync(request).Result;
            return response;
        }
    }
}
