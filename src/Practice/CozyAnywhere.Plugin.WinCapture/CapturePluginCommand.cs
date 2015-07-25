using CozyAnywhere.Plugin.WinCapture.Args;
using CozyAnywhere.Plugin.WinCapture.ArgsFactory;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CozyAnywhere.Plugin.WinCapture
{
    public partial class CapturePlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            MethodDictionary["GetCaptureData"] = new GetCaptureDataArgsFactory();
        }

        public static string MakeGetCaptureDataCommand()
        {
            var args            = new GetCaptureDataArgs();
            var argsSerialize   = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "GetCaptureData", argsSerialize);
        }
    }
}