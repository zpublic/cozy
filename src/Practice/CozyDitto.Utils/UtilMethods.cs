using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyDitto.Utils
{
    public static partial class Util
    {
        // bool RegisterShowWindowHotKey(HWND hWnd, UINT fsModifiers, UINT vk);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RegisterShowWindowHotKey(IntPtr hWnd, KeyModifiers fsModifiers, VirtualKey vk);

        // bool UnregisterShowWindowHotKey(HWND hWnd);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnregisterShowWindowHotKey(IntPtr hWnd);

        // int GetShowWindowHotKeyId();
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetShowWindowHotKeyId();

        // bool SetClipboardText(HWND hWnd, LPCTSTR lpText, DWORD dwLength);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetClipboardText(IntPtr hWnd, string text, uint length);

        public static bool SetClipboardText(IntPtr hWnd, string text)
        {
            return SetClipboardText(hWnd, text, (uint)text.Length * 2);
        }

        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetClipboardText(IntPtr hWnd, StringBuilder result);

        public static string GetClipboardText(IntPtr hWnd)
        {
            uint size = GetClipboardSize(hWnd);
            StringBuilder sb = new StringBuilder((int)size);
            GetClipboardText(hWnd, sb);
            return sb.ToString();
        }

        public static uint GetClipboardSize(IntPtr hWnd)
        {
            return GetClipboardText(hWnd, null);
        }
    }
}
