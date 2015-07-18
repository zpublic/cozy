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
            uint offset     = 0;
            int width       = 0;
            int height      = 0;
            var bitmapData  = CaptureUtil.DefGetCaptureData(ref offset, ref width, ref height);

            var data = CaptureUtil.SplitBitmap(bitmapData, offset, 32, 32, width, height);

            BITMAP bmp = new BITMAP();
            CaptureUtil.GetWindowBitmapSize(ref bmp);
            bmp.bmWidth = 32;
            bmp.bmHeight = 32;
            bmp.bmWidthBytes = 4 * 32;
            foreach(var obj in data)
            {
                var jpg = new byte[offset + obj.Data.Length];
                CaptureUtil.AppendBitmapHeader(ref jpg[0], ref bmp);
                Array.Copy(obj.Data, 0, jpg, offset, obj.Data.Length);
                obj.Data = jpg;
            }

            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.PacketBinaryDataType,
                Data        = new PluginMehtodReturnValuePacket()
                {
                    Packet = data,
                },
            };
        }
    }
}
