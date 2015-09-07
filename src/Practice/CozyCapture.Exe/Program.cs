using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyCapture.Exe
{
    class Program
    {
        const uint FLG_TOCLIP   = 1;
        const uint FLG_TOFILE   = 2;

        const uint RET_FAILED   = 0;
        const uint RET_CLIP     = 1;
        const uint RET_FILE     = 2;

        [DllImport(@"CozyCapture.Base.dll",CharSet =CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetCaptureImage(uint dwFlags, string lpFileName, ref uint lpResultState);

        static void Main(string[] args)
        {
            uint result = 0;
            GetCaptureImage(FLG_TOCLIP | FLG_TOFILE, @"result.bmp", ref result);
            if (result == RET_FAILED)
            {
                Console.WriteLine("Failed");
            }
            else 
            {
                if ((result & RET_CLIP) != 0)
                {
                    Console.WriteLine("Clip Complete");
                }

                if ((result & RET_FILE) != 0)
                {
                    Console.WriteLine("File Complete");
                }
            }
            Console.ReadKey();
        }
    }
}
