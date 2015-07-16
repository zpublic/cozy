using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System;

namespace CozyAnywhere.Plugin.WinCapture
{
    public static class CaptureUtil
    {
        // DWORD GetWindowBitmapSize();
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint GetWindowBitmapSize();

        // bool GetCaptureData(LPBYTE lpResult);
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern bool GetCaptureData(ref byte result);

        #region DefaultMethod

        public static byte[] DefGetCaptureData()
        {
            uint size       = GetWindowBitmapSize();
            if (size == 0) return null;

            byte[] result   = new byte[size];
            if(!GetCaptureData(ref result[0]))
            {
                return null;
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