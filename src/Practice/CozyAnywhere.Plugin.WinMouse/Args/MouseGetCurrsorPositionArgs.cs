using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinMouse.Args
{
    public class MouseGetCurrsorPositionArgs : IPluginCommandMethodArgs
    {
        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (MousePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}