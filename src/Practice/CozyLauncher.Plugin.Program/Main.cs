using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Program
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
            if (query.RawQuery == "hehe")
            {
                var rl  = new List<Result>();
                var r   = new Result();
                r.Title     = "Hehe";
                r.SubTitle  = "关闭";
                r.IcoPath   = "exe";
                r.Score     = 100;
                r.Action    = e =>
                {
                    context_.Api.CloseApp();
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            else if (query.RawQuery == "calc")
            {
                var rl  = new List<Result>();
                var r   = new Result();
                r.Title     = "Calc";
                r.SubTitle  = "打开计算器";
                r.IcoPath   = "exe";
                r.Score     = 100;
                r.Action    = e =>
                {
                    context_.Api.HideApp();
                    Process.Start("calc");
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }
    }
}
