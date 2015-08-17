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
        // bool RegisterHotKeyWithName(LPCTSTR lpId ,UINT fsModifiers, UINT vk)
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RegisterHotKeyWithName(string name, KeyModifiers fsModifiers, VirtualKey vk);

        // bool UnregisterHotKeyWithName(LPCTSTR lpId);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnregisterHotKeyWithName(string name);

        // int GetHotKeyIdWithName(LPCTSTR lpName)
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHotKeyIdWithName(string name);

        // bool SetClipboardText(LPCTSTR lpText, DWORD dwLength);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetClipboardText(string text, uint length);

        public static bool SetClipboardText(string text)
        {
            return SetClipboardText(text, (uint)text.Length * 2);
        }

        // DWORD GetClipboardText(LPTSTR lpResult);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetClipboardText(StringBuilder result);

        public static string GetClipboardText()
        {
            uint size = GetClipboardSize();
            StringBuilder sb = new StringBuilder((int)size);
            GetClipboardText(sb);
            return sb.ToString();
        }

        public static uint GetClipboardSize()
        {
            return GetClipboardText(null);
        }

        // bool CreateHideMessageWindow()
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CreateHideMessageWindow();

        // void EnterMessageLoop();
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnterMessageLoop();

        // void SetHotKeyCallback(HotKeyCallBack callback);
        [DllImport(@"CozyDitto.Core.dll",
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetHotKeyCallback(HotKeyCallback callback);
    }
}
