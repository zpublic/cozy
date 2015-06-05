using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CozyMobi.Core.Model;
using System.Net;
using CozyMobi.Core.RequestBuilder;

namespace CozyMobi.Core.Network
{
    class HttpPost
    {
        public static HttpResponseMessage Post(string url, HttpContent content)
        {
            Uri uri = new Uri(url);
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            if (RequestBuilderCommon.AccountLogin != url)
            {
                handler.CookieContainer.Add(uri, AccountInfo.Instance.Cookies);
                handler.CookieContainer.Add(AccountInfo.Instance.Cookies);
            }
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.PostAsync(uri, content).Result;
            if (RequestBuilderCommon.AccountLogin == url)
            {
                var v = response.Headers.GetValues("Set-Cookie");
                AccountInfo.Instance.Cookies = handler.CookieContainer.GetCookies(uri);
            }
            return response;
        }
    }
}
