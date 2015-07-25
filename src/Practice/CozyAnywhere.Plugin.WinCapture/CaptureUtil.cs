using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;
using CozyAnywhere.Plugin.WinCapture.Model;

namespace CozyAnywhere.Plugin.WinCapture
{
    public static class CaptureUtil
    {
        // DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap)
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint AppendBitmapHeader(ref byte data, ref BITMAP bitmap);

        // bool GetWindowHDC(HWND *lpHwnd, HDC *lpHdc);
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern bool GetWindowHDC(ref IntPtr hwnd, ref IntPtr hdc);

        //DWORD GetHdcCaptureData(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBYTE lpResult, LPBITMAP lpBitmap);
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint GetCaptureData(IntPtr hwnd, IntPtr hdc, int x, int y, int w, int h, ref byte result);

        //DWORD GetHDCCaptureDataSize(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBITMAP lpBitmap)
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern uint GetCaptureDataSize(IntPtr hwnd, IntPtr hdc, int x, int y, int w, int h, ref BITMAP bmp);

        //void GetWindowSize(HWND hwnd, LPLONG x, LPLONG y);
        [DllImport(@"CaptureCpp.dll",
           CharSet              = CharSet.Auto,
           CallingConvention    = CallingConvention.Cdecl)]
        public static extern void GetWindowSize(IntPtr hwnd, ref int x, ref int y);

        #region DefaultMethod

        public static byte[] DefGetCaptureData(IntPtr hwnd, IntPtr hdc, int x, int y, int w, int h)
        {
            BITMAP bmp  = new BITMAP();
            uint offset = 0;
            uint size   = GetCaptureDataSize(hwnd, hdc, x, y, w, h, ref bmp);
            if (size == 0)
            {
                offset = 0;
                return null;
            }

            byte[] data = new byte[size];
            offset      = AppendBitmapHeader(ref data[0], ref bmp);
            if (offset == 0)
            {
                return null;
            }
            if (GetCaptureData(hwnd, hdc, x, y, w, h, ref data[offset]) == 0)
            {
                return null;
            }
            return data;
        }

        public static byte[] ConvertBmpToJpeg(byte[] input)
        {
            using (MemoryStream ims = new MemoryStream(input))
            {
                Bitmap bm = (Bitmap)Image.FromStream(ims);
                using (MemoryStream oms = new MemoryStream())
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