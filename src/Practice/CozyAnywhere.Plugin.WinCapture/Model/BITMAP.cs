using System;
using System.Runtime.InteropServices;

namespace CozyAnywhere.Plugin.WinCapture.Model
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAP
    {
        public int bmType;

        public int bmWidth;

        public int bmHeight;

        public int bmWidthBytes;

        public int bmPlanes;

        public int bmBitsPixel;

        public IntPtr bmBits;
    }
}