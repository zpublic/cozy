using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CozyMabi.Core.Model;
using System.Net;
using CozyMabi.Core.RequestBuilder;

namespace CozyMabi.Core.Network
{
    class HttpPost
    {
        public static HttpResponseMessage Post(string url, HttpContent content)
        {
            Uri uri = new Uri(RequestBuilderCommon.Host);
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
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (RequestBuilderCommon.AccountLogin == url)
            {
                var c = handler.CookieContainer.GetCookies(uri);
                foreach (Cookie e in c)
                {
                    HttpCookie.InternetSetCookie(RequestBuilderCommon.Host, e.Name, e.Value + ";path=/;expires=Sun,22-Feb-2099 00:00:00 GMT");
                }
            }
            return response;
        }
    }
}
