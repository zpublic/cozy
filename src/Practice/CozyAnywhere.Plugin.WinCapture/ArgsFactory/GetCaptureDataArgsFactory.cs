using CozyAnywhere.Plugin.WinCapture.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinCapture.ArgsFactory
{
    public class GetCaptureDataArgsFactory : IPluginCommandMethodArgsFactory
    {
        public IPluginCommandMethodArgs Create(string argsContent)
        {
            return JsonConvert.DeserializeObject<GetCaptureDataArgs>(argsContent);
        }
    }
}