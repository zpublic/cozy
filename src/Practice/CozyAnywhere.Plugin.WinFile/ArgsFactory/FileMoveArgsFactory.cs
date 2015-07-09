using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.ArgsFactory
{
    public class FileMoveArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            var result = new FileMoveArgs();

            return result;
        }
    }
}