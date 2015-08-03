using CozyAnywhere.Plugin.WinProcess.Args;
using CozyAnywhere.Plugin.WinProcess.ArgsFactory;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;
using PluginHelper;

namespace CozyAnywhere.Plugin.WinProcess
{
    public partial class ProcessPlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            var asm         = Assembly.GetExecutingAssembly();
            var factorylist = ArgsFactoryLoader.LoadArgsFactory(asm, "CozyAnywhere.Plugin.WinProcess.ArgsFactory");

            foreach (var obj in factorylist)
            {
                var factory                 = (IPluginCommandMethodArgsFactory)Activator.CreateInstance(obj.Item2);
                MethodDictionary[obj.Item1] = factory;
            }
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