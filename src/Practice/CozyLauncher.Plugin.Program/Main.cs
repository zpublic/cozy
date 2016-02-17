using CozyLauncher.Plugin.Program.ProgramSource;
using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;
using CozyLauncher.Infrastructure;

namespace CozyLauncher.Plugin.Program
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;
        private List<ISource> SourceList { get; set; } = new List<ISource>();
        private List<string> FileList { get; set; } = new List<string>();
        private List<string> FolderList { get; set; } = new List<string>();

        public override PluginInfo Init(PluginInitContext context)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x =>
            {
                return x.Namespace == @"CozyLauncher.Plugin.Program.ProgramSource" && x.GetInterface("ISource") != null;
            });

            foreach (var type in types)
            {
                ISource inst = null;
                try
                {
                    inst = (ISource)Activator.CreateInstance(type);
                }
                catch
                {
                    continue;
                }
                if (inst != null)
                {
                    SourceList.Add(inst);
                }
            }

            Update();

            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";
            return info;
        }

        private List<string> FileCache { get; set; } = new List<string>();

        public override List<Result> Query(Query query)
        {
            var matcher = FuzzyMatcher.Create(query.RawQuery);

            var ret = new List<Result>();
            foreach(var file in FileCache)
            {
                var fn = Path.GetFileName(file);
                var ans = matcher.Evaluate(fn);
                if (ans.Success)
                {
                    ret.Add(CreateResult(file, ans.Score));
                }
                else
                {
                    var pyans = matcher.EvaluatePinYin(fn);
                    if (pyans.Success)
                    {
                        ret.Add(CreateResult(file, pyans.Score));
                    }
                }
            }
            return ret;
        }

        private void Update()
        {
            foreach (var source in SourceList)
            {
                FileCache.AddRange(source.LoadProgram());
            }

            FileCache = FileCache.Where(x => x.EndsWith(".exe") || x.EndsWith(".bat") || x.EndsWith(".lnk")).Select(x => x.ToLower()).Distinct().ToList();
        }

        private Result CreateResult(string path, int source)
        {
            var res = new Result()
            {
                SubTitle = path,
                Score = source,
                Action = x =>
                {
                    Process.Start(path);
                    context_.Api.HideAndClear();
                    return true;
                },
                Title = Path.GetFileNameWithoutExtension(path),
                IcoPath = path,
            };
            
            return res;
        }
    }
}
