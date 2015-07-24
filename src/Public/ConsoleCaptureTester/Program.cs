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
            IntPtr hwnd = IntPtr.Zero;
            IntPtr hdc = IntPtr.Zero;

            if (CaptureUtil.GetWindowHDC(ref hwnd, ref hdc))
            {
                int x = 0;
                int y = 0;
                CaptureUtil.GetWindowSize(hwnd, ref x, ref y);

                // 4k / 4 = 1k
                // sqrt(1024) = 32
                int num = 0;
                const int blockSize = 128;
                int blockSizeW = (x + blockSize - 1) / blockSize;
                int blockSizeH = (y + blockSize - 1) / blockSize;
                for (int i = 0; i < blockSizeW; ++i)
                {
                    for (int j = 0; j < blockSizeH; ++j)
                    {
                        var r = CaptureUtil.DefGetCaptureData(hwnd, hdc, i * blockSize, j * blockSize, blockSize + i * blockSize, blockSize + j * blockSize);
                        var result = CaptureUtil.ConvertBmpToJpeg(r);
                        using (FileStream fs = new FileStream(@"D:\Test\test" + num + @".jpg", FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(result, 0, result.Length);
                        }
                        ++num;
                    }
                }
            }
        }
    }
}