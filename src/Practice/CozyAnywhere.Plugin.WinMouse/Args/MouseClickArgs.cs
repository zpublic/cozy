using CozyAnywhere.Plugin.WinMouse.Tag;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseClickArgs : PluginCommandMethodArgs
    {
        public ButtonTag Tag { get; set; }

        public uint X { get; set; }

        public uint Y { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}