using CozyAnywhere.Plugin.WinProcess.Args;
using CozyAnywhere.Plugin.WinProcess.ArgsFactory;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CozyAnywhere.Plugin.WinProcess
{
    public partial class ProcessPlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            MethodDictionary["ProcessCreate"]               = new ProcessCreateArgsFactory();
            MethodDictionary["ProcessEnum"]                 = new ProcessEnumArgsFactory();
            MethodDictionary["ProcessGetName"]              = new ProcessGetNameArgsFactory();
            MethodDictionary["ProcessTerminate"]            = new ProcessTerminateArgsFactory();
            MethodDictionary["ProcessTerminateWithTimeOut"] = new ProcessTerminateWithTimeOutArgsFactory();
        }

        public static string MakeProcessCreateCommand(string path)
        {
            var args = new ProcessCreateArgs()
            {
                Path = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "ProcessCreate", argsSerialize);
        }

        public static string MakeProcessEnumCommand()
        {
            var args = new ProcessEnumArgs();
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "ProcessEnum", argsSerialize);
        }

        public static string MakeProcessGetNameCommand(uint pid)
        {
            var args = new ProcessGetNameArgs()
            {
                Pid = pid,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "ProcessGetName", argsSerialize);
        }

        public static string MakeProcessTerminateCommand(uint pid)
        {
            var args = new ProcessTerminateArgs()
            {
                Pid = pid,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "ProcessTerminate", argsSerialize);
        }

        public static string MakeProcessTerminateWithTimeOutCommand(uint pid, uint timeout)
        {
            var args = new ProcessTerminateWithTimeOutArgs()
            {
                Pid         = pid,
                TimeOut     = timeout,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "ProcessTerminateWithTimeOut", argsSerialize);
        }
    }
}