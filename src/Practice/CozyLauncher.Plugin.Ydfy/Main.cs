using CozyLauncher.PluginBase;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.Ydfy
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        private const string translteUrl = "http://fanyi.youdao.com/openapi.do?keyfrom=CozyLauncher&key=1040605813&type=data&doctype=json&version=1.1&q=";

        public override PluginInfo Init(PluginInitContext context)
        {
            this.context_ = context;
            var info = new PluginInfo();
            info.Keyword = "ydfy";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("ydfy "))
            {
                var result = new List<Result>();
                var queryString = query.RawQuery.Substring(5);

                // fix bug:防止Request的时候出现Json异常
                if (queryString.Equals(""))
                {
                    return null;
                }

                var model = Request(queryString);
                if (model.ErrorCode == 0)
                {
                    result.Add(new Result
                    {
                        Title = model.Translation[0],
                        SubTitle = "Copy this text to the clipboard",
                        IcoPath = "txt",
                        Score = 90,
                        Action = e =>
                        {
                            context_.Api.HideAndClear();
                            try
                            {
                                Clipboard.SetText(model.Translation[0]);
                                return true;
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return false;
                            }
                        }
                    });
                    if (model.Detail != null)
                    {
                        result.AddRange(model.Detail.Explains
                            .Select(x => new Result
                            {
                                Title = x,
                                SubTitle = "Copy this text to the clipboard",
                                IcoPath = "[Res]:txt",
                                Score = 80,
                                Action = e =>
                                {
                                    context_.Api.HideAndClear();
                                    try
                                    {
                                        Clipboard.SetText(x);
                                        return true;
                                    }
                                    catch (System.Runtime.InteropServices.ExternalException)
                                    {
                                        return false;
                                    }
                                }
                            }));
                    }
                    return result;
                }
            }
            return null;
        }

        private TranslateModel Request(string query)
        {
            using (var client = new HttpClient())
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, translteUrl + query);
                var respones = client.SendAsync(requestMsg).Result;
                var jsonString = respones.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TranslateModel>(jsonString);
                return result;
            }
        }
    }
}