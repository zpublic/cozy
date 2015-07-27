using Newtonsoft.Json;

namespace CozyAnywhere.Protocol
{
    public static class PluginCommandSerializeMaker
    {
        public static string MakeCommand(string pluginName, string methodName, string args)
        {
            var method = new PluginCommandMethod()
            {
                MethodName = methodName,
                MethodArgs = args,
            };
            var command = new PluginCommand()
            {
                PluginName = pluginName,
                PluginCommandContent = JsonConvert.SerializeObject(method),
            };
            var result = JsonConvert.SerializeObject(command);
            return result;
        }
    }
}
