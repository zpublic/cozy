using CozyAnywhere.Plugin.WinProcess.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinProcess
{
    public partial class ProcessPlugin
    {
        public PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public PluginMethodReturnValueType Shell(ProcessCreateArgs args)
        {
            var result = ProcessUtil.ProcessCreate(args.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(ProcessEnumArgs args)
        {
            var result = ProcessUtil.DefProcessEnum();
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(ProcessGetNameArgs args)
        {
            var result = ProcessUtil.DefGetProcessName(args.Pid);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(ProcessTerminateArgs args)
        {
            var result = ProcessUtil.ProcessTerminate(args.Pid);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(ProcessTerminateWithTimeOutArgs args)
        {
            var result = ProcessUtil.ProcessTerminateWithTimeOut(args.Pid, args.TimeOut);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }
    }
}