using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;
using CozyAnywhere.Plugin.WinCapture.Args;
using System.Text;
using CozyAnywhere.PluginBase;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinCapture.Model;
using System.Drawing;
using System.IO;
using PluginHelper;
using System.Drawing.Imaging;

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
                var bitmapSize      = CaptureUtil.DefGetCaptureBlockBitmap(hwnd, hdc, 0, 0, 0, 0, ref bitmap);
                var bmp = CaptureUtil.DefGetCaptureData(hwnd, hdc, bitmapSize, bitmap, 0, 0, x, y);
                using (MemoryStream ms = new MemoryStream(bmp))
                {
                    var pic = (Bitmap)Image.FromStream(ms);
                    var locker = new BitmapLocker(pic);
                    locker.LockBits();
                    for (int i = 0; i < blockSizeW; ++i)
                    {
                        for (int j = 0; j < blockSizeH; ++j)
                        {
                            Bitmap outputBmp = new Bitmap(blockSize, blockSize);
                            var outputLocker = new BitmapLocker(outputBmp);
                            outputLocker.LockBits();
                            int blockBeginX = i * blockSize;
                            int blockBeginY = j * blockSize;
                            int blockWidth  = (i + 1) * (blockSize) > x ? (x % blockSize) : blockSize;
                            int blockHeight = (j + 1) * (blockSize) > y ? (y % blockSize) : blockSize;
                            for (int k = 0; k < blockWidth; ++k)
                            {
                                for (int l = 0; l < blockHeight; ++l)
                                {
                                    outputLocker.SetPixel(k, l, locker.GetPixel(blockBeginX + k, blockBeginY + l));
                                }
                            }
                            outputLocker.UnlockBits();
                            using (MemoryStream oms = new MemoryStream())
                            {
                                outputBmp.Save(oms, ImageFormat.Jpeg);
                                byte[] data = new byte[oms.Length];
                                oms.Seek(0, SeekOrigin.Begin);
                                oms.Read(data, 0, data.Length);
                                var meta = new CaptureSplitMetaData()
                                {
                                    X = blockBeginX,
                                    Y = blockBeginY,
                                    Width = blockSize,
                                    Height = blockSize,
                                };

                                var m = JsonConvert.SerializeObject(meta);
                                result.Add(new ReturnValuePacket()
                                {
                                    MetaData = m,
                                    Data = data,
                                });
                            }
                        }
                    }
                    locker.UnlockBits();
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
