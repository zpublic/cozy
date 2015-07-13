using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;
using System.Collections.Generic;
using System.Linq;

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
    }
}