using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CozyLauncher.Core.Plugin
{
    public class PluginMgr
    {
        List<IPlugin> plugins_ = new List<IPlugin>();
        IPublicApi api_ = null;

        public void Init(IPublicApi api)
        {
            api_ = api;

            PluginInitContext context = new PluginInitContext();
            context.Api = api;

            var PluginFilleList = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .Where(x => Path.GetFileName(x).StartsWith("CozyLauncher.Plugin.") && x.EndsWith(".dll"));

            foreach (var p in PluginFilleList)
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(p);
                    List<Type> types = asm.GetTypes().Where(o => o.IsClass && !o.IsAbstract && o.GetInterfaces().Contains(typeof(IPlugin))).ToList();
                    foreach (Type type in types)
                    {
                        var plugin = Activator.CreateInstance(type) as IPlugin;
                        plugin.Init(context);
                        plugins_.Add(plugin);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void Query(Query query)
        {
            List<Result> results = new List<Result>();
            foreach (var p in plugins_)
            {
                var r = p.Query(query);
                var e = r?.AsEnumerable();
                if (e != null)
                    results.AddRange(e);
            }
            results.Sort(CompareResult);
            api_?.PushResults(results);
        }

        public void ShowPanel(string command)
        {
            foreach (var p in plugins_)
            {
                p.ShowPanel(command);
            }
        }

        private static int CompareResult(Result x, Result y)
        {
            return y.Score.CompareTo(x.Score);
        }
    }
}
