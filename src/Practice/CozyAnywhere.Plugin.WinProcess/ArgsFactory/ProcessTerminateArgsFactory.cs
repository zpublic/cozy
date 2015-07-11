using CozyAnywhere.Plugin.WinProcess.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinProcess.ArgsFactory
{
    public class ProcessTerminateArgsFactory : IPluginCommandMethodArgsFactory
    {
        public PluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<ProcessTerminateArgs>(argsContent);
        }
    }
}