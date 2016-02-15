using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public static class HotkeyNative
    {
        [StructLayout(LayoutKind.Sequential)]
        internal class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 256;

        private static int HookHandle { get; set; }

        public static Func<int, int, bool> ProcessCallback { get; set; }

        private static int KbHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (ProcessCallback != null && wParam == WM_KEYDOWN)
                {
                    // hhkb XD
                    var hhkb = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));

                    return ProcessCallback(hhkb.vkCode, hhkb.scanCode) ? 1 : 0;
                }
            }

            return CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }

        private static HookProc HookProcHelper { get; set; }

        public static void Init()
        {
            if(HookHandle != 0)
            {
                throw new Exception("already set hotkey hook");
            }
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    HookProcHelper = new HookProc(KbHookProc);
                    HookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, HookProcHelper, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        public static void Release()
        {
            if (HookHandle != 0)
            {
                UnhookWindowsHookEx(HookHandle);
                HookHandle = 0;
            }
        }
    }
}
