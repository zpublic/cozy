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
            uint offset = 0;
            int width = 0;
            int height = 0;
            var result  = CaptureUtil.ConvertBmpToJpeg(CaptureUtil.DefGetCaptureData(ref offset, ref width, ref height));
            using (FileStream fs = new FileStream(@"D:\test.jpg", FileMode.Create, FileAccess.Write))
            {
                fs.Write(result, 0, result.Length);
            }
            //Console.ReadKey();

            var bitmapData = CaptureUtil.DefGetCaptureData(ref offset, ref width, ref height);
            BITMAP bmp = new BITMAP();
            CaptureUtil.GetWindowBitmapSize(ref bmp);

            var data = CaptureUtil.SplitBitmap(bitmapData, offset, 32, 32, width, height);

            
            bmp.bmWidth = 32;
            bmp.bmHeight = 32;
            bmp.bmWidthBytes = 4 * 32;
            foreach (var obj in data)
            {
                var jpg = new byte[offset + obj.Data.Length];
                CaptureUtil.AppendBitmapHeader(ref jpg[0], ref bmp);
                Array.Copy(obj.Data, 0, jpg, offset, obj.Data.Length);
                obj.Data = jpg;
            }
            using (FileStream fs = new FileStream(@"D:\test.bmp", FileMode.Create, FileAccess.Write))
            {
                fs.Write(data[100].Data, 0, data[0].Data.Length);
            }
            Console.ReadKey();
        }
    }
}