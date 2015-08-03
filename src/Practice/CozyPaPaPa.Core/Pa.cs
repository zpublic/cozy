using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CozyPaPaPa.Core
{
    public class Pa
    {
        public event KeyEventHandler KeyDownEvent;
        public event KeyEventHandler KeyUpEvent;
        public event KeyPressEventHandler KeyPressEvent;

        public void Start()
        {
            if (mhKeyboardHook == 0)
            {
                KeyboardHookProcedure = new KeyboardHook.HookProc(KeyboardHookProc);
                mhKeyboardHook = KeyboardHook.SetWindowsHookEx(
                    KeyboardHook.WH_KEYBOARD_LL,
                    KeyboardHookProcedure,
                    KeyboardHook.GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
            }
        }

        public void Stop()
        {
            if (mhKeyboardHook != 0)
            {
                KeyboardHook.UnhookWindowsHookEx(mhKeyboardHook);
                mhKeyboardHook = 0;
            }
        }
        
        private static int mhKeyboardHook = 0;
        private KeyboardHook.HookProc KeyboardHookProcedure;

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (KeyDownEvent != null || KeyUpEvent != null || KeyPressEvent != null))
            {
                KeyboardHook.KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHook.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHook.KeyboardHookStruct));
                if (KeyDownEvent != null && (wParam == KeyboardHook.WM_KEYDOWN || wParam == KeyboardHook.WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDownEvent(this, e);
                }
                
                if (KeyPressEvent != null && wParam == KeyboardHook.WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    KeyboardHook.GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];
                    if (KeyboardHook.ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        KeyPressEvent(this, e);
                    }
                }
                
                if (KeyUpEvent != null && (wParam == KeyboardHook.WM_KEYUP || wParam == KeyboardHook.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUpEvent(this, e);
                }

            }
            return KeyboardHook.CallNextHookEx(mhKeyboardHook, nCode, wParam, lParam);
        }
    }
}
