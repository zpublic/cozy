using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;
using CozyAnywhere.Plugin.WinCapture.Args;
using System.Text;
using CozyAnywhere.PluginBase;

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
            uint offset     = 0;
            var bitmapData  = CaptureUtil.DefGetCaptureData(out offset);
            if (bitmapData != null)
            {
                var jpedData = CaptureUtil.ConvertBmpToJpeg(bitmapData);
                if(jpedData != null)
                {
                    return new PluginMethodReturnValueType()
                    {
                        DataType    = PluginMethodReturnValueType.BinaryDataType,
                        Data        = jpedData,
                    };
                }
            }
            return null;
        }
    }
}
