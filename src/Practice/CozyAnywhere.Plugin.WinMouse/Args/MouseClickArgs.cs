using CozyAnywhere.Plugin.WinMouse.Tag;
using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseClickArgs : IPluginCommandMethodArgs
    {
        public ButtonTag Tag { get; set; }

        public uint X { get; set; }

        public uint Y { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}