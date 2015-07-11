using CozyAnywhere.PluginBase;
using System.Collections.Generic;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.PluginMgr
{
    public class PluginManager
    {
        private Dictionary<string, BasePlugin> PluginDictionary = new Dictionary<string, BasePlugin>();
        private object objLocker                                = new object();

        public PluginCommandMethodReturnValue ShellPluginCommand(string pluginName, string commandContent)
        {
            BasePlugin plugin = null;
            lock (objLocker)
            {
                if (PluginDictionary.ContainsKey(pluginName))
                {
                    plugin = PluginDictionary[pluginName];
                }
            }
            if(plugin != null)
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

        public void AddPlugin(string pluginName, BasePlugin plugin)
        {
            lock(objLocker)
            {
                PluginDictionary[pluginName] = plugin;
            }
        }
    }
}