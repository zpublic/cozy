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
        [DllImport(@"CozyCapture.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CreateCaptureWindow();

        [DllImport(@"CozyCapture.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnterMainLoop();

        static void Main(string[] args)
        {
            CreateCaptureWindow();
            EnterMainLoop();
        }
    }
}
