using System;
using System.Collections.Generic;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Core.Plugin
{
    public class LuaPluginLoader : IPluginLoader
    {
        string file_;

        public LuaPluginLoader(string file)
        {
            file_ = file;
        }

        public List<IPlugin> GetPlugins(PluginInitContext context)
        {
            List<IPlugin> ps = new List<IPlugin>();
            try
            {
                var plugin = new LuaPluginWrapper();
                if (plugin.Load(file_))
                {
                    plugin.Init(context);
                    ps.Add(plugin);
                }
            }
            catch (Exception)
            {
            }
            return ps;
        }
    }
}
