using System;
using System.Net.Http;
using System.Net;

namespace CozyInfrastructure.HttpClient
{
    public class HttpPost
    {
        public static HttpResponseMessage Post(string host, string url, HttpContent content, bool needSaveCookie = false)
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (needSaveCookie)
            {
                var c = handler.CookieContainer.GetCookies(uri);
                foreach (Cookie e in c)
                {
                    HttpCookie.InternetSetCookie(host, e.Name, e.Value + ";path=/;expires=Sun,22-Feb-2099 00:00:00 GMT");
                }
            }
            return response;
        }
    }
}
