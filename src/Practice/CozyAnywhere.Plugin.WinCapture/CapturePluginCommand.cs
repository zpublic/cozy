using CozyAnywhere.Plugin.WinCapture.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using PluginHelper;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace CozyAnywhere.Plugin.WinCapture
{
    public partial class CapturePlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            var asm         = Assembly.GetExecutingAssembly();
            var factorylist = ArgsFactoryLoader.LoadArgsFactory(asm, "CozyAnywhere.Plugin.WinCapture.ArgsFactory");
            foreach (var obj in factorylist)
            {
                var factory                 = (IPluginCommandMethodArgsFactory)Activator.CreateInstance(obj.Item2);
                MethodDictionary[obj.Item1] = factory;
            }
        }

        public static string MakeGetCaptureDataCommand()
        {
            var args            = new GetCaptureDataArgs();
            var argsSerialize   = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "GetCaptureData", argsSerialize);
        }
    }
}