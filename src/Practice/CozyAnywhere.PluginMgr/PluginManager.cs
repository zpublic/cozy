using CozyAnywhere.PluginBase;
using System.Collections.Generic;

namespace CozyAnywhere.PluginMgr
{
    public class PluginManager
    {
        private Dictionary<string, BasePlugin> PluginDictionary = new Dictionary<string, BasePlugin>();
        private object objLocker = new object();

        public void ShellPluginCommand(string pluginName, IPluginCommand command)
        {
            if (!PluginDictionary.ContainsKey(pluginName))
            {
                lock (objLocker)
                {
                    if (!PluginDictionary.ContainsKey(pluginName))
                    {
                        // TODO Try Add Plugin
                    }
                }
            }
            var plugin = PluginDictionary[pluginName];
            plugin.Dispatch(command);
        }
    }
}