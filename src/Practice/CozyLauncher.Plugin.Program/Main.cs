using CozyLauncher.Plugin.Program.ProgramSource;
using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;

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

            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            var res = new List<string>();
            foreach (var source in SourceList)
            {
                res.AddRange(source.LoadProgram());
            }

            var dis = res.Where(x => x.EndsWith(".exe") || x.EndsWith(".bat") || x.EndsWith(".lnk")).Select(x => x.ToLower()).Distinct();

            var matcher = FuzzyMatcher.Create(query.RawQuery);
            var ret =  dis.Where(x => 
            {
                var name = Directory.Exists(x) ? new DirectoryInfo(x).Name : Path.GetFileName(x);
                return matcher.Evaluate(name).Success;
            }).Select(x => CreateResult(x)).Distinct().ToList();

            return ret;
        }

        private Result CreateResult(string path)
        {
            var res = new Result()
            {
                SubTitle = path,
                Score = 50,
                Action = x =>
                {
                    Process.Start(path);
                    context_.Api.HideAndClear();
                    return true;
                },
            };

            if(Directory.Exists(path))
            {
                res.Title = new DirectoryInfo(path).Name;
                res.IcoPath = "folder_open";
            }
            else
            {
                res.Title = Path.GetFileNameWithoutExtension(path);
                res.IcoPath = "app";
            }
            
            return res;
        }
    }
}
