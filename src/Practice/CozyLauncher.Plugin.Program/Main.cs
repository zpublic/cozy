using CozyLauncher.Plugin.Program.ProgramSource;
using CozyLauncher.PluginBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;

namespace CozyLauncher.Plugin.Program
{
    public class Main : IPlugin
    {
        private PluginInitContext context_;
        private List<ISource> SourceList { get; set; } = new List<ISource>();

        public PluginInfo Init(PluginInitContext context)
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

        internal class ResultComparer : IEqualityComparer<Result>
        {
            public bool Equals(Result x, Result y)
            {
                return x.SubTitle.ToLower() == y.SubTitle.ToLower();
            }

            public int GetHashCode(Result obj)
            {
                return obj.GetHashCode();
            }
        }

        public List<Result> Query(Query query)
        {
            var res = new List<Result>();
            foreach(var source in SourceList)
            {
                res.AddRange(source.LoadProgram(query));
            }
            res.Distinct(new ResultComparer());

            foreach(var obj in res)
            {
                obj.Action = x =>
                {
                    Process.Start(obj.SubTitle);
                    context_.Api.HideAndClear();
                    return true;
                };
            }
            return res;
        }
    }
}
