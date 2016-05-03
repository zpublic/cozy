using System;
using RestSharp;
using CozyFarseer.TopList.Model;

namespace CozyFarseer.TopList.Network
{
    public static class TopListUpdate
    {
        public const string DefaultUserAnget    = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
        public const string BaseUrl             = @"http://imxz.net:12580/";

        public static readonly Random RandomGen = new Random();

        public static void Last(Action<FarseerTopList> callback)
        {
            var client  = new RestClient(BaseUrl);
            var request = new RestRequest("count/GetTopList?num=20&pid=0&tagid=0", Method.GET);

            request.AddHeader("User-Agent", DefaultUserAnget);

            client.ExecuteAsync<FarseerTopList>(request, resp => 
            {
                callback?.Invoke(resp.Data);
            });
        }
    }
}
