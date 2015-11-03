using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBored.Client.Core
{
    public class BoredApi
    {
        const string BaseUrl = "http://www.laorouji.com:1024";

        public static int QueryRank()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("query-rank/1", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return 0;
        }

        public static int GetRank(int time)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("get-rank/1/" + time.ToString(), Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return 0;
        }

        public static bool Save(string name, int time)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("save", Method.POST);
            request.AddParameter("ver", 1);
            request.AddParameter("name", name);
            request.AddParameter("time", time);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return true;
        }
    }
}
