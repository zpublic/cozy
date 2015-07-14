using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinMouse
{
    public class LoadHelper : IPluginLoadHelper
    {
        public string PluginName { get { return MousePlugin.InnerPluginName; } }
    }
}