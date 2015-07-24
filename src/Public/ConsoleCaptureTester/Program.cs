using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using CozyAnywhere.Plugin.WinCapture;
using CozyAnywhere.Plugin.WinCapture.Model;

namespace ConsoleCaptureTester
{
    // CaptureCpp.dll
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var r = CaptureUtil.DefGetCaptureData(ref offset, ref width, ref height);
            var r = CaptureUtil.DefGetCaptureData(0, 0, 0, 0);
            var result  = CaptureUtil.ConvertBmpToJpeg(r);
            using (FileStream fs = new FileStream(@"D:\test.jpg", FileMode.Create, FileAccess.Write))
            {
                fs.Write(result, 0, result.Length);
            }
            Console.ReadKey();
        }
    }
}