using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinCapture.Args
{
    public class GetCaptureDataArgs : IPluginCommandMethodArgs
    {
        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (CapturePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}