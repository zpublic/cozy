using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.WebSearch
{
    public class Main : IPlugin
    {
        private PluginInitContext context_;

        public PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "g";
            return info;
        }

        public List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("g "))
            {
                var s = query.RawQuery.Substring(2);
                var rl = new List<Result>();
                var r = new Result();
                r.Title = s;
                r.SubTitle = "Search Google";
                r.IcoPath = "google";
                r.Score = 70;
                r.Action = e =>
                {
                    context_.Api.HideApp();
                    Process.Start("https://www.google.com/?gws_rd=ssl#q=" + s);
                    return true;
                };
                var r2 = new Result();
                r2.Title = s;
                r2.SubTitle = "Search Baidu";
                r2.IcoPath = "baidu";
                r2.Score = 69;
                r2.Action = e =>
                {
                    context_.Api.HideApp();
                    Process.Start("https://www.baidu.com/s?wd=" + s);
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
