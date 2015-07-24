namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandMethodArgsFactory
    {
        // TODO deserialization Args By argsContent
        IPluginCommandMethodArgs Create(string argsContent);
    }
}