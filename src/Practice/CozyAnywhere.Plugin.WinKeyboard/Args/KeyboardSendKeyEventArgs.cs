using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinKeyboard.Args
{
    public class KeyboardSendKeyEventArgs : IPluginCommandMethodArgs
    {
        public VirtualKey Key { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (KeyboardPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}