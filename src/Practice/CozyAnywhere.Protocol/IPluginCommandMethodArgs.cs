namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandMethodArgs
    {
        string Execute(IPluginCommandArgsDispatch dispatch);
    }
}