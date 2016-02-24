using CozyLauncher.PluginBase;
using FluentScheduler;
using System.Collections.Generic;

namespace CozyLauncher.Plugin.BreakReminder
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            TaskManager.Initialize(new TaskRegistry());

            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "xiuxi";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "xiuxi")
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = "Config BreakReminder";
                r.SubTitle = @"Open the BreakReminder`s setting panel";
                r.IcoPath = "[Res]:sys";
                r.Score = 100;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }
    }
}
