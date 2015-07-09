using CozyAnywhere.Plugin.WinMouse.Tag;
using System.Runtime.InteropServices;

namespace CozyAnywhere.Plugin.WinMouse
{
    public static class MouseUtil
    {
        // bool GetCursorPosition(long* lpXPosition, long* lpYPosition);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool GetCursorPosition(ref int x, ref int y);

        // bool SetCursorPosition(int iXPosition, int iYPosition);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool SetCursorPosition(int x, int y);

        // bool CursorClip(long lLeft, long lTop, long lRight, long lBottom);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool CursorClip(int left, int top, int right, int bottom);

        // bool CursorUnClip();
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool CursorUnClip();

        // void MouseEvent(DWORD dwFlags, DWORD dx, DWORD dy, DWORD dwData, ULONG_PTR dwExtraInfo);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void MouseEvent(MouseEventTag flags, uint x, uint y, uint data, uint ExtInfo);

        // void MouseClick(BUTTON key, DWORD dx, DWORD dy);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void MouseClick(ButtonTag tag, uint x, uint y);

        // void LeftClick(DWORD dx, DWORD dy)
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void LeftClick(uint x, uint y);

        // void RightClick(DWORD dx, DWORD dy);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void RightClick(uint x, uint y);

        // void MiddleClick(DWORD dx, DWORD dy);
        [DllImport(@"MouseUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void MiddleClick(uint x, uint y);
    }
}