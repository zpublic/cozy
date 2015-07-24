using CozyAnywhere.Plugin.WinMouse.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinMouse.ArgsFactory
{
    public class MouseCursorUnClipArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<MouseCursorUnClipArgs>(argsContent);
        }
    }
}