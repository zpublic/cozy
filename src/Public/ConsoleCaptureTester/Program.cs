using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using CozyAnywhere.Plugin.WinCapture;

namespace ConsoleCaptureTester
{
    // CaptureCpp.dll
    internal class Program
    {
        [DllImport(@"CaptureCpp.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetWindowBitmapSize();

        [DllImport(@"CaptureCpp.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetCaptureData(ref byte result, uint size);

        private static void Main(string[] args)
        {
            var result = CaptureUtil.ConvertBmpToJpeg(CaptureUtil.DefGetCaptureData());
            using (FileStream fs = new FileStream(@"D:\test.jpg", FileMode.Create, FileAccess.Write))
            {
                fs.Write(result, 0, result.Length);
            }
            Console.ReadKey();
        }
    }
}