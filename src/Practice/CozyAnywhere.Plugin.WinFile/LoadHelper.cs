using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile
{
    public class LoadHelper : IPluginLoadHelper
    {
        public string PluginName { get { return FilePlugin.InnerPluginName; } }
    }
}