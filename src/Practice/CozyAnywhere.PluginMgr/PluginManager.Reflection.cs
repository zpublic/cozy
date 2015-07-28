using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;
using System;
using System.Reflection;

namespace CozyAnywhere.PluginMgr
{
    public partial class PluginManager
    {
        public void AddPluginWithFileName(string filename)
        {
            if (filename.StartsWith("CozyAnywhere.Plugin.") && filename.EndsWith(".dll"))
            {
                string ns = filename.Substring(0, filename.Length - 4);
                Assembly assembly = Assembly.LoadFrom(filename);

                Type loadhelper = assembly.GetType(ns + ".LoadHelper");
                IPluginLoadHelper helper = (IPluginLoadHelper)Activator.CreateInstance(loadhelper, null);

                string pluginName = helper.PluginName;
                Type pluginType = assembly.GetType(ns + "." + pluginName);
                IPlugin plugin = (IPlugin)Activator.CreateInstance(pluginType, null);
                AddPlugin(plugin);
            }
        }
    }
}
