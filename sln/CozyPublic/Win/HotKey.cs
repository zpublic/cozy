using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CozyPublic.Win
{
    public class HotKey
    {
        public const int ModifierAlt        = 1;
        public const int ModifierControl    = 2;
        public const int ModifierShift      = 4;
        public const int ModifierWindows    = 8;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
              IntPtr hWnd,
              int id,
              uint fsModifiers,
              uint vk
              );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
              IntPtr hWnd,
              int id
              );
    }
}
