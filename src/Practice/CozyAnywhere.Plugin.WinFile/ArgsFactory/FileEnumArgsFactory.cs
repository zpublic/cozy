using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileEnumArgsFactory : IPluginCommandMethodArgsFactory
    {
        public PluginCommandMethodArgs Create(string argsContent)
        {
            var result = JsonConvert.DeserializeObject<FileEnumArgs>(argsContent);
            return result;
        }
    }
}