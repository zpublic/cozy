using CozyAnywhere.Plugin.WinKeyboard.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinKeyboard.ArgsFactory
{
    public class KeyboardEventArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<KeyboardEventArgs>(argsContent);
        }
    }
}