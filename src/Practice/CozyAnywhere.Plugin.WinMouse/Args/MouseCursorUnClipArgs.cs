using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseCursorUnClipArgs : IPluginCommandMethodArgs
    {
        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}