using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileGetTimesArgsFactory : IPluginCommandMethodArgsFactory
    {
        public PluginCommandMethodArgs Create(string argsContent)
        {
            var result = JsonConvert.DeserializeObject<FileGetTimesArgs>(argsContent);
            return result;
        }
    }
}