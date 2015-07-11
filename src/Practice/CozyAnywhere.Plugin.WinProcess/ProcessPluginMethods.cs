using CozyAnywhere.Plugin.WinProcess.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System;

namespace CozyAnywhere.Plugin.WinProcess
{
    public partial class ProcessPlugin
    {
        public string Dispatch(PluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public string Shell(PluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public string Shell(ProcessCreateArgs args)
        {
            var result = ProcessUtil.ProcessCreate(args.Path);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(ProcessEnumArgs args)
        {
            var result = ProcessUtil.DefProcessEnum();
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(ProcessGetNameArgs args)
        {
            var result = ProcessUtil.DefGetProcessName(args.Pid);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(ProcessTerminateArgs args)
        {
            var result = ProcessUtil.ProcessTerminate(args.Pid);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(ProcessTerminateWithTimeOutArgs args)
        {
            var result = ProcessUtil.ProcessTerminateWithTimeOut(args.Pid, args.TimeOut);
            return JsonConvert.SerializeObject(result);
        }
    }
}