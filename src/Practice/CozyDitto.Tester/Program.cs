using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyDitto.Utils;
using System.Threading;

namespace CozyDitto.Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Util.RegisterHotKeyWithName("SetClipboard", Util.KeyModifiers.Ctrl, VirtualKey.VK_F2);
            Util.SetHotKeyCallback((w, l) =>
            {
                if (w == Util.GetHotKeyIdWithName("SetClipboard"))
                {
                    Util.SetClipboardText("cozy zui diao");
                    return true;
                }
                return false;
            });

            Util.EnterMessageLoop();

            Util.UnregisterHotKeyWithName("SetClipboard");
        }
    }
}
