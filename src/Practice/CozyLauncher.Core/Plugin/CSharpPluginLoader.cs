using System;
using System.Collections.Generic;
using System.Linq;
using CozyLauncher.PluginBase;
using System.Reflection;

namespace CozyLauncher.Core.Plugin
{
    public class CSharpPluginLoader : IPluginLoader
    {
        string file_;

        public CSharpPluginLoader(string file)
        {
            file_ = file;
        }

        public List<IPlugin> GetPlugins(PluginInitContext context)
        {
            List<IPlugin> ps = new List<IPlugin>();
            try
            {
                Assembly asm = Assembly.LoadFile(file_);
                List<Type> types = asm.GetTypes()
                    .Where(o => o.IsClass && !o.IsAbstract && o.GetInterfaces()
                    .Contains(typeof(IPlugin)))
                    .ToList();
                foreach (var type in types)
                {
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    if (plugin != null)
                    {
                        plugin.Init(context);
                        ps.Add(plugin);
                    }
                }
            }
            catch (Exception)
            {
            }
            return ps;
        }
    }
}
