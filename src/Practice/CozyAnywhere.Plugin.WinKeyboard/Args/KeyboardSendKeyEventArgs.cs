using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinKeyboard.Args
{
    public class KeyboardSendKeyEventArgs : IPluginCommandMethodArgs
    {
        public VirtualKey Key { get; set; }

        public string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (KeyboardPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}