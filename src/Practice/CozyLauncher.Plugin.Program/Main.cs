using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
            var rl = new List<Result>();

            var EnvVar = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach (var path in EnvVar)
            {
                var ActPath = Path.Combine(path, query.RawQuery + ".exe");
                if (File.Exists(ActPath))
                {
                    var r = new Result()
                    {
                        Title = query.RawQuery,
                        SubTitle = ActPath,
                        IcoPath = "exe",
                        Score = 100,
                        Action = e =>
                        {
                            context_.Api.HideApp();
                            Process.Start(ActPath);
                            return true;
                        },
                    };
                    rl.Add(r);
                }
            }

            if(rl.Count > 0)
            {
                return rl;
            }

            return null;
        }
    }
}
