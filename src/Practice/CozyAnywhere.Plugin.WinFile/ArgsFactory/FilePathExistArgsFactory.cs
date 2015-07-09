using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FilePathExistArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            var result = new FilePathExistArgs();

            return result;
        }
    }
}