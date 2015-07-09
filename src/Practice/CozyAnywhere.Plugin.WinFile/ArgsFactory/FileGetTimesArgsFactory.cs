using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileGetTimesArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            var result = new FileGetTimesArgs();

            return result;
        }
    }
}