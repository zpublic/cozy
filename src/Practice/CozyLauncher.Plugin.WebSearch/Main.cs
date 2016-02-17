using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.WebSearch
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        private string google
        {
            get
            {
                return "https://www.google.com";
            }
        }

        private string baiduUrl
        {
            get
            {
                return "https://www.baidu.com";
            }
        }

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "g";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            
            if (query.RawQuery.StartsWith("g "))
            {
                var s = query.RawQuery.Substring(2);
                var rl = new List<Result>();
                var r = new Result();
                r.Title = s;
                r.SubTitle = "Search Google";
                r.IcoPath = google+ "/favicon.ico";
                r.Score = 70;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    try
                    {
                        Process.Start( google + "/?gws_rd=ssl#q=" + s);
                    }
                    catch (Exception) { }
                    return true;
                };
                var r2 = new Result();
                r2.Title = s;
                r2.SubTitle = "Search Baidu";
                r2.IcoPath = "[Res]:baidu"/*baiduUrl + "/favicon.ico"*/;
                r2.Score = 69;
                r2.Action = e =>
                {
                    context_.Api.HideAndClear();
                    try
                    {
                        Process.Start( baiduUrl + "/s?wd=" + s);
                    }
                    catch (Exception) { }
                    return true;
                };
                rl.Add(r);
                rl.Add(r2);
                return rl;
            }
            return null;
        }
    }
}
