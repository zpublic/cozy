using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileGetLengthArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            var result = new FileGetLengthArgs();

            return result;
        }
    }
}