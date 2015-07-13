using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinKeyboard.Args
{
    public class KeyboardEventArgs : IPluginCommandMethodArgs
    {
        // VirtualKey Key, byte ScanKey, uint Flag, uint ExtraInfo
        public VirtualKey Key { get; set; }

        public byte ScanKey { get; set; }

        public uint Flag { get; set; }

        public uint ExtraInfo { get; set; }

        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (KeyboardPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}