using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinCapture.Args
{
    public class GetCaptureDataArgs : IPluginCommandMethodArgs
    {
        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (CapturePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}