namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandMethodArgsFactory
    {
        // TODO deserialization Args By argsContent
        PluginCommandMethodArgs Create(string argsContent);
    }
}