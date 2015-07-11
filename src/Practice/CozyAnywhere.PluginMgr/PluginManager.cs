using CozyAnywhere.PluginBase;
using System.Collections.Generic;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.PluginMgr
{
    public class PluginManager
    {
        private Dictionary<string, IPlugin> PluginDictionary = new Dictionary<string, IPlugin>();
        private object objLocker                                = new object();

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

        public void AddPlugin(IPlugin plugin)
        {
            lock(objLocker)
            {
                PluginDictionary[plugin.PluginName] = plugin;
            }
        }
    }
}