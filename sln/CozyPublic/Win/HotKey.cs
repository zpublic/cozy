using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozyPublic.Win
{
    public class HotKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
              IntPtr hWnd,
              int id,
              ModifierKeys fsModifiers,
              int vk
              );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
              IntPtr hWnd,
              int id
              );
    }
}
