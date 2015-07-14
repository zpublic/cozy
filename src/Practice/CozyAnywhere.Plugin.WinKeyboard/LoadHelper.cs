using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinKeyboard
{
    public class LoadHelper : IPluginLoadHelper
    {
        public string PluginName { get { return KeyboardPlugin.InnerPluginName; } }
    }
}