using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyDict.Core
{
    public static class MouseUtil
    {
        public delegate IntPtr MouseHookCallback(int code, UIntPtr wParam, IntPtr lParam);


        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetMouseHook(MouseHookCallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnSetMouseHook();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InvalidateMouseWindow(int x, int y);

        
    }
}
