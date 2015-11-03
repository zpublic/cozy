using CozyBored.Client.Core.Model;
using RestSharp;
using System.Collections.Generic;

namespace CozyBored.Client.Core
{
    public class BoredApi
    {
        const string Ver = "1";
        const string BaseUrl = "http://www.laorouji.com:23333/";
        //const string BaseUrl = "http://localhost:23333/";

        public static List<BoredModel> QueryRank()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.GET);
            request.Resource = "query-rank/{ver}";
            request.AddUrlSegment("ver", Ver);
            var response = client.Execute(request);
            var result = SimpleJson.DeserializeObject<List<BoredModel>>(response.Content);
            return result;
        }

        public static int GetRank(double time)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.GET);
            request.Resource = "get-rank/{ver}/{time}";
            request.AddUrlSegment("ver", Ver);
            request.AddUrlSegment("time", time.ToString());
            var response = client.Execute(request);
            int result = (int)(SimpleJson.DeserializeObject<dynamic>(response.Content).num);
            return result;
        }

        public static bool Save(BoredModel model)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("save/{ver}", Method.POST);
            request.AddUrlSegment("ver", Ver);
            request.AddObject(model);
            IRestResponse response = client.Execute(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
