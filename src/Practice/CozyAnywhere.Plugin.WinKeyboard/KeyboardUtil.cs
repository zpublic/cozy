using System.Runtime.InteropServices;

namespace CozyAnywhere.Plugin.WinKeyboard
{
    public static class KeyboardUtil
    {
        // void KeyboardEvent(WORD wKey, WORD wScanKey, DWORD dwFlag, DWORD dwExtraInfo);
        [DllImport(@"KeyboardUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void KeyboardEvent(VirtualKey Key, byte ScanKey, uint Flag, uint ExtraInfo);

        // bool QueryKeyState(WORD wKey);
        [DllImport(@"KeyboardUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool QueryKeyState(VirtualKey Key);

        // void SendKeyEvent(WORD wKey);
        [DllImport(@"KeyboardUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void SendKeyEvent(VirtualKey Key);
    }
}