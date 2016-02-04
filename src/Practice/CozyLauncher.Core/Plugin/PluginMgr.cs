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

            var PluginFilleList = new List<string>()
            {
                // Core
                "CozyLauncher.Plugin.Core.dll",

                // Primary
                "CozyLauncher.Plugin.Program.dll",
                "CozyLauncher.Plugin.ManualRun.dll",
                "CozyLauncher.Plugin.Dirctory.dll",
                "CozyLauncher.Plugin.WebSearch.dll",
                "CozyLauncher.Plugin.Sys.dll",
                "CozyLauncher.Plugin.Calculator.dll",

                // Secondary
                "CozyLauncher.Plugin.MouseClick.dll",
                "CozyLauncher.Plugin.Ip.dll",
            };

            foreach (var p in PluginFilleList)
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, p));
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
            api_?.PushResults(results);
        }
    }
}
