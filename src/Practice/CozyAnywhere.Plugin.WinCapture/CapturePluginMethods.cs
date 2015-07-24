using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;
using CozyAnywhere.Plugin.WinCapture.Args;
using System.Text;
using CozyAnywhere.PluginBase;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinCapture.Model;

namespace CozyAnywhere.Plugin.WinCapture
{
    public partial class CapturePlugin
    {
        public PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public PluginMethodReturnValueType Shell(GetCaptureDataArgs CopyArgs)
        {
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.PacketBinaryDataType,
                Data        = new PluginMehtodReturnValuePacket()
                {
                    Packet = null,
                },
            };
        }
    }
}
