using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandMethodArgs
    {
        PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch);
    }
}