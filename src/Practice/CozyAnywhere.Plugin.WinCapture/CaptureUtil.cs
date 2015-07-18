using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System;
using CozyAnywhere.Plugin.WinCapture.Model;
using System.Collections.Generic;
using CozyAnywhere.PluginBase;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinCapture
{
    public static class CaptureUtil
    {
        // DWORD GetWindowBitmapSize();
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint GetWindowBitmapSize(ref BITMAP bitmap);

        // bool GetCaptureData(LPBYTE lpResult);
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint GetCaptureData(ref byte result, ref BITMAP bitmap);

        // DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap)
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint AppendBitmapHeader(ref byte data, ref BITMAP bitmap);

        #region DefaultMethod

        public static byte[] DefGetCaptureData(ref uint offset, ref int width, ref int height)
        {
            BITMAP bitmap = new BITMAP(); ;
            uint size = GetWindowBitmapSize(ref bitmap);
            if (size == 0)
            {
                offset = 0;
                return null;
            }
            byte[] result   = new byte[size];

            offset = AppendBitmapHeader(ref result[0], ref bitmap);
            if(offset == 0)
            {
                return null;
            }
            if(GetCaptureData(ref result[offset], ref bitmap) == 0)
            {
                return null;
            }
            width = bitmap.bmWidth;
            height = bitmap.bmHeight;
            return result; 
        }

        public static List<ReturnValuePacket> SplitBitmap(byte[] data, uint offset, int blockwidth, int blockheight, int width, int height)
        {
            var result = new List<ReturnValuePacket>();
            for (int i = 0; i < width; i += blockwidth)
            {
                for (int j = 0; j < height; j += blockheight)
                {
                    int count = 0;
                    byte[] d = new byte[offset + blockwidth * blockheight * 4];
                    for (int k = 0; k < blockwidth; ++k)
                    {
                        for (int l = 0; l < blockheight; ++l)
                        {
                            d[count++] = data[offset + ((i + k) * width) + j + l + 0];
                            d[count++] = data[offset + ((i + k) * width) + j + l + 1];
                            d[count++] = data[offset + ((i + k) * width) + j + l + 2];
                            d[count++] = data[offset + ((i + k) * width) + j + l + 3];
                        }
                    }
                    result.Add(new ReturnValuePacket()
                    {
                        Data = d,
                        MetaData = JsonConvert.SerializeObject(new CaptureSplitMetaData()
                        {
                            X = i,
                            Y = j,
                            Width = blockwidth,
                            Height = blockheight,
                        }),
                    });
                }
            }
            return result;
        }

        public static byte[] ConvertBmpToJpeg(byte[] input)
        {
            using(MemoryStream ims = new MemoryStream(input))
            {
                Bitmap bm = (Bitmap)Image.FromStream(ims);
                using(MemoryStream oms = new MemoryStream())
                {
                    bm.Save(oms, ImageFormat.Jpeg);
                    byte[] result = new byte[oms.Length];
                    oms.Seek(0, SeekOrigin.Begin);
                    oms.Read(result, 0, result.Length);
                    return result;
                }
            }
        }

        #endregion
    }
}