using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinCapture
{
    public class LoadHelper : IPluginLoadHelper
    {
        public string PluginName { get { return CapturePlugin.InnerPluginName; } }
    }
}