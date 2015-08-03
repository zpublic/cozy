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
            List<ReturnValuePacket> result = new List<ReturnValuePacket>();

            IntPtr hwnd = IntPtr.Zero;
            IntPtr hdc = IntPtr.Zero;

            if (CaptureUtil.GetWindowHDC(ref hwnd, ref hdc))
            {
                int x = 0;
                int y = 0;
                CaptureUtil.GetWindowSize(hwnd, ref x, ref y);

                const int blockSize = 128;
                int blockSizeW      = (x + blockSize - 1) / blockSize;
                int blockSizeH      = (y + blockSize - 1) / blockSize;
                var bitmap          = new BITMAP();
                var bitmapSize      = CaptureUtil.DefGetCaptureBlockBitmap(hwnd, hdc, 0, 0, blockSize, blockSize, ref bitmap);
                for (int i = 0; i < blockSizeW; ++i)
                {
                    for (int j = 0; j < blockSizeH; ++j)
                    {
                        var bmp     = CaptureUtil.DefGetCaptureData(hwnd, hdc, bitmapSize, bitmap, i * blockSize, j * blockSize, blockSize, blockSize);
                        var jpg     = CaptureUtil.ConvertBmpToJpeg(bmp);
                        var meta    = new CaptureSplitMetaData()
                        {
                            X       = i * blockSize,
                            Y       = j * blockSize,
                            Width   = blockSize,
                            Height  = blockSize,
                        };

                        var m = JsonConvert.SerializeObject(meta);
                        result.Add(new ReturnValuePacket()
                        {
                            MetaData    = m,
                            Data        = jpg,
                        });
                    }
                }
            }

            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.PacketBinaryDataType,
                Data        = new PluginMehtodReturnValuePacket()
                {
                    Packet = result,
                },
            };
        }
    }
}
