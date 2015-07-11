using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileIsDirectoryArgsFactory : IPluginCommandMethodArgsFactory
    {
        public PluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<FileIsDirectoryArgs>(argsContent);
        }
    }
}