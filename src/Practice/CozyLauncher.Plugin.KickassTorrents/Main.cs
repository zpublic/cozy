using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.Net.Http;

namespace CozyLauncher.Plugin.KickassTorrents {

    public class Main : BasePlugin {

        private PluginInitContext context_;

        private const string torrentsUrl = "https://kat.cr/usearch/{0}/?rss=1";

        public override PluginInfo Init(PluginInitContext context) {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "t";
            return info;
        }

        public override List<Result> Query(Query query) {
            if (query.RawQuery.StartsWith("t ")) {
                var result = new List<Result>();
                var querySrting = query.RawQuery.Substring(2);
                var respones = Request(querySrting);
            }
            return null;
        }

        private string Request(string query) {
            using (var client = new HttpClient()) {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, string.Format(torrentsUrl, query));
                var respones = client.SendAsync(requestMsg).Result;
                var byteArray = respones.Content.ReadAsByteArrayAsync().Result;
                //var jsonString = respones.Content.ReadAsStringAsync().Result;
                foreach (var item in Encoding.GetEncodings()) {
                    var t = item.GetEncoding().GetString(byteArray);
                }
                var test = Encoding.Unicode.GetString(byteArray);
                return string .Empty;
            }
        }
    }
}
