using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileIsDirectoryArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            var result = new FileIsDirectoryArgs();

            return result;
        }
    }
}