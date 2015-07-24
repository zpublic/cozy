using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinProcess
{
    public class LoadHelper : IPluginLoadHelper
    {
        public string PluginName { get { return ProcessPlugin.InnerPluginName; } }
    }
}