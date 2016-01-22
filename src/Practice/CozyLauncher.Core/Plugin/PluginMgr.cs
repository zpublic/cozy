using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
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

            try
            {
                Assembly asm = Assembly.Load(AssemblyName.GetAssemblyName("./CozyLauncher.Plugin.Program.dll"));
                List<Type> types = asm.GetTypes().Where(o => o.IsClass && !o.IsAbstract && o.GetInterfaces().Contains(typeof(IPlugin))).ToList();
                foreach (Type type in types)
                {
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    plugin.Init(context);
                    plugins_.Add(plugin);
                }

                Assembly asm1 = Assembly.Load(AssemblyName.GetAssemblyName("./CozyLauncher.Plugin.ManualRun.dll"));
                List<Type> types1 = asm1.GetTypes().Where(o => o.IsClass && !o.IsAbstract && o.GetInterfaces().Contains(typeof(IPlugin))).ToList();
                foreach (Type type in types1)
                {
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    plugin.Init(context);
                    plugins_.Add(plugin);
                }

                Assembly asm2 = Assembly.Load(AssemblyName.GetAssemblyName("./CozyLauncher.Plugin.Dirctory.dll"));
                List<Type> types2 = asm2.GetTypes().Where(o => o.IsClass && !o.IsAbstract && o.GetInterfaces().Contains(typeof(IPlugin))).ToList();
                foreach (Type type in types2)
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
