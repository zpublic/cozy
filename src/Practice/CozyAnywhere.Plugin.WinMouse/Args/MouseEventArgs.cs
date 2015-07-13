using CozyAnywhere.Plugin.WinMouse.Tag;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseEventArgs : IPluginCommandMethodArgs
    {
        public MouseEventTag Tag { get; set; }

        public uint X { get; set; }

        public uint Y { get; set; }

        public uint Data { get; set; }

        public uint ExtInfo { get; set; }

        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}