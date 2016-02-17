using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Dirctory
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;
        private Dictionary<string, Environment.SpecialFolder> paths_ = new Dictionary<string, Environment.SpecialFolder>();

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            InitPathList();
            var info = new PluginInfo();
            info.Keyword = "p";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            Environment.SpecialFolder f;
            if (paths_.TryGetValue(query.RawQuery, out f))
            {
                var path = Environment.GetFolderPath(f);
                var rl = new List<Result>();
                var r = new Result();
                r.Title = f.ToString();
                r.SubTitle = path;
                r.IcoPath = "[Res]:folder_open";
                r.Score = 50;
                r.Action = e =>
                {
                    Process.Start("explorer.exe", path);
                    context_.Api.HideAndClear();

                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }

        void InitPathList()
        {
            paths_.Add("p zm", Environment.SpecialFolder.Desktop);
            paths_.Add("p cookie", Environment.SpecialFolder.Cookies);
            paths_.Add("p font", Environment.SpecialFolder.Fonts);
            paths_.Add("p history", Environment.SpecialFolder.History);
            paths_.Add("p doc", Environment.SpecialFolder.MyDocuments);
            paths_.Add("p prog", Environment.SpecialFolder.ProgramFiles);
            paths_.Add("p start", Environment.SpecialFolder.StartMenu);
            paths_.Add("p sys", Environment.SpecialFolder.System);
            paths_.Add("p win", Environment.SpecialFolder.Windows);
        }
    }
}
