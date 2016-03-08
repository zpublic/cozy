using CozyLauncher.PluginBase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.DailySentence
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;
        private const string url_ = "http://open.iciba.com/dsapi";

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "mryj";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "mryj")
            {
                var model = Request(url_);
                var rl = new List<Result>();
                var r = new Result();
                r.Title = model.note;
                r.SubTitle = model.content;
                r.IcoPath = "[Res]:txt";
                r.Score = 100;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    try
                    {
                        Clipboard.SetText(r.SubTitle);
                        return true;
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return false;
                    }
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }

        private DailySentenceModel Request(string query)
        {
            using (var client = new HttpClient())
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, query);
                var respones = client.SendAsync(requestMsg).Result;
                var jsonString = respones.Content.ReadAsStringAsync().Result;
                var jo = JObject.Parse(jsonString);
                var r = new DailySentenceModel()
                {
                    content = jo?.GetValue("content").ToString(),
                    note = jo?.GetValue("note").ToString(),
                };
                return r;
            }
        }
    }
}
