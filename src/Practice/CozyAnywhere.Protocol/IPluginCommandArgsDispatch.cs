using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandArgsDispatch
    {
        PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args);

        PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args);
    }
}
