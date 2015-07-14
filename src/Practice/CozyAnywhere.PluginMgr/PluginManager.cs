using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace CozyAnywhere.PluginMgr
{
    public class PluginManager
    {
        private Dictionary<string, IPlugin> PluginDictionary = new Dictionary<string, IPlugin>();
        private object objLocker = new object();

        public PluginCommandMethodReturnValue ShellPluginCommand(string pluginName, string commandContent)
        {
            IPlugin plugin = null;
            lock (objLocker)
            {
                if (PluginDictionary.ContainsKey(pluginName))
                {
                    plugin = PluginDictionary[pluginName];
                }
            }
            if (plugin != null)
            {
                return plugin.Shell(commandContent);
            }
            return null;
        }

        public PluginCommandMethodReturnValue ParsePluginCommand(string command)
        {
            var pluginCommand = PluginCommand.CreateWithParse(command);
            return ShellPluginCommand(pluginCommand.PluginName, pluginCommand.PluginCommandContent);
        }

        public void AddPlugin(IPlugin plugin)
        {
            lock (objLocker)
            {
                if (!ContainsPlugin(plugin.PluginName))
                {
                    PluginDictionary[plugin.PluginName] = plugin;
                }
            }
        }

        public bool ContainsPlugin(string name)
        {
            return PluginDictionary.ContainsKey(name);
        }

        public List<string> AllPluginName()
        {
            return PluginDictionary.Keys.ToList();
        }

        public void AddPluginsWithFileNames(List<string> files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    AddPluginWithFileName(file);
                }
            }
        }

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