using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.BrowserBookmark
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "b";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("b "))
            {
                var rl = new List<Result>();
                return rl;
            }
            return null;
        }
    }
}
