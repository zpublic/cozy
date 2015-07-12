using CozyAnywhere.Plugin.WinKeyboard.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinKeyboard.ArgsFactory
{
    public class KeyboardQueryKeyStateArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<KeyboardQueryKeyStateArgs>(argsContent);
        }
    }
}