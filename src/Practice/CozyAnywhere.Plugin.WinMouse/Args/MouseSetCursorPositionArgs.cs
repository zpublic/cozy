using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseSetCursorPositionArgs : IPluginCommandMethodArgs
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}