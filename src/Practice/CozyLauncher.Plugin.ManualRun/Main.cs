using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.ManualRun
{
    public class Main : IPlugin
    {
        private PluginInitContext context_;
        private Dictionary<string, ActionOpenDirctory> actions1_ = new Dictionary<string, ActionOpenDirctory>();
        private Dictionary<string, ActionOpenExe> actions2_ = new Dictionary<string, ActionOpenExe>();

        public PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";

            actions1_.Add("win", new ActionOpenDirctory()
            {
                Key = "win",
                Dirctory = @"c:\windows",
            });
            actions2_.Add("cc", new ActionOpenExe()
            {
                Key = "cc",
                Exe = "calc",
            });

            return info;
        }

        public List<Result> Query(Query query)
        {
            ActionOpenDirctory acDir;
            if (actions1_.TryGetValue(query.RawQuery, out acDir))
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = acDir.Key;
                r.SubTitle = acDir.Dirctory;
                r.IcoPath = "folder_open";
                r.Score = 100;
                r.Action = e =>
                {
                    Process.Start("explorer.exe", acDir.Dirctory);
                    context_.Api.HideAndClear();
                    return true;
                };
                rl.Add(r);
                return rl;
            }

            ActionOpenExe acExe;
            if (actions2_.TryGetValue(query.RawQuery, out acExe))
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = acExe.Key;
                r.SubTitle = acExe.Exe;
                r.IcoPath = "app";
                r.Score = 100;
                r.Action = e =>
                {
                    context_.Api.HideApp();
                    Process.Start(acExe.Exe, acExe.Param);
                    return true;
                };
                rl.Add(r);
                return rl;
            }

            return null;
        }
    }
}
