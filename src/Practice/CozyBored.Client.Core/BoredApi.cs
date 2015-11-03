using CozyBored.Client.Core.Model;
using RestSharp;
using System.Collections.Generic;

namespace CozyBored.Client.Core {

    public class BoredApi {

        const string BaseUrl = "http://www.laorouji.com:1024";
        //const string BaseUrl = "http://localhost:1024/";

        public static List<BoredModel> QueryRank(string ver) {

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.GET);
            request.Resource = "query-rank/{ver}";
            request.AddUrlSegment("ver", ver);
            var response = client.Execute(request);
            var result = SimpleJson.DeserializeObject<List<BoredModel>>(response.Content);
            return result;
        }

        public static int GetRank(string ver, int time) {

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.GET);
            request.Resource = "get-rank/{ver}/{time}";
            request.AddUrlSegment("ver", ver);
            request.AddUrlSegment("time", time.ToString());
            var response = client.Execute(request);
            int result = (int)(SimpleJson.DeserializeObject<dynamic>(response.Content).num);
            return result;
        }

        public static bool Save(BoredModel model) {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("save", Method.POST);
            request.AddObject(model);
            IRestResponse response = client.Execute(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
