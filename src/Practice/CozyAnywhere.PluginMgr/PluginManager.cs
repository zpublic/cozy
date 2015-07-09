using CozyAnywhere.PluginBase;
using System.Collections.Generic;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.PluginMgr
{
    public class PluginManager
    {
        private Dictionary<string, BasePlugin> PluginDictionary = new Dictionary<string, BasePlugin>();
        private object objLocker                                = new object();

        public void ShellPluginCommand(string pluginName, string commandContent)
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
                plugin.Shell(commandContent);
            }
        }

        public void ParsePluginCommand(string command)
        {
            var pluginCommand = PluginCommand.CreateWithParse(command);
            ShellPluginCommand(pluginCommand.PluginName, pluginCommand.PluginCommandContent);
        }
    }
}