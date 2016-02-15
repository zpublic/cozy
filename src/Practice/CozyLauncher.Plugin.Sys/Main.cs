using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Sys
{
    public class Main : IPlugin
    {
        private PluginInitContext context_;

        public PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";
            return info;
        }

        public List<Result> Query(Query query)
        {
            if (query.RawQuery == "host" || query.RawQuery == "hosts")
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = "Hosts";
                r.SubTitle = "open hosts file";
                r.IcoPath = "sys";
                r.Score = 90;
                r.Action = e =>
                {
                    Process.Start("notepad", Environment.GetFolderPath(Environment.SpecialFolder.System) + "/drivers/etc/hosts");
                    context_.Api.HideAndClear();
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            else if (query.RawQuery == "logoff")
            {
            }
            return null;
        }
    }
}
