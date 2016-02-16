using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CozyLauncher.PluginBase;
using Newtonsoft.Json;

namespace CozyLauncher.Plugin.Ydfy {

    public class Main : BasePlugin {

        private PluginInitContext context;

        private const string translteUrl = "http://fanyi.youdao.com/openapi.do?keyfrom=CozyLauncher&key=1040605813&type=data&doctype=json&version=1.1&q=";

        public override PluginInfo Init(PluginInitContext context) {
            this.context = context;
            var info = new PluginInfo();
            info.Keyword = "ydfy";
            return info;
        }

        public override List<Result> Query(Query query) {
            if (query.RawQuery.StartsWith("ydfy ")) {
                var result = new List<Result>();
                var queryString = query.RawQuery.Substring(5);
                var model = Request(queryString);
                if (model.ErrorCode == 0) {
                    result.Add(new Result { Title = model.Translation[0] });
                    if (model.Detail != null) {
                        result.AddRange(model.Detail.Explains.Select(x => new Result { Title = x }));
                    }
                    return result;
                }
            }
            return null;
        }

        private TranslateModel Request(string query) {
            using (var client = new HttpClient()) {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, translteUrl + query);
                var respones = client.SendAsync(requestMsg).Result;
                var jsonString = respones.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TranslateModel>(jsonString);
                return result;
            }
        }
    }
}
