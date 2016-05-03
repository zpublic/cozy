using System;
using RestSharp;
using CozyFarseer.TopList.Model;

namespace CozyFarseer.TopList.Network
{
    public static class TopListUpdate
    {
        public const string DefaultUserAnget    = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
        public const string BaseUrl             = @"http://imxz.net:12580/";

        public static void Last(Action<IRestResponse<FarseerTopList>> callback, int count = 20, int pid = 0, int tagid = 0)
        {
            var client          = new RestClient(BaseUrl);

            var request         = new RestRequest(MakeUrl(count, pid, tagid), Method.GET);
            request.Timeout     = 5000;

            request.AddHeader("User-Agent", DefaultUserAnget);

            client.ExecuteAsync<FarseerTopList>(request, resp => 
            {
                callback?.Invoke(resp);
            });
        }

        private static string MakeUrl(int count, int pid, int tagid)
        {
            var startTime   = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            int t           = (int)(DateTime.Now - startTime).TotalSeconds;

            return $"count/GetTopList?num={count}&pid={pid}&tagid={tagid}&t={t}";
        }

        public static void Load(string url, Action<IRestResponse> callback)
        {
            var client = new RestClient(BaseUrl);

            var request = new RestRequest(url, Method.GET);
            request.Timeout = 5000;

            request.AddHeader("User-Agent", DefaultUserAnget);

            client.ExecuteAsync<FarseerTopList>(request, resp =>
            {
                callback?.Invoke(resp);
            });
        }
    }
}
