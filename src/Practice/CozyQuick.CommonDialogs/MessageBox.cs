using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyQuick.CommonDialogs
{
    public static class MessageBox
    {
        public static int Show(IntPtr hWnd, string lpText, string lpCaption, int uType)
        {
            return CozyPublic.WinApi.WindowsAPI.MessageBox(hWnd, lpText, lpCaption, uType);
        }
    }
}
