using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseCursorClipArgs : IPluginCommandMethodArgs
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}