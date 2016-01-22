using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public struct ModifyKeyStatus
    {
        public bool Ctrl { get; set; }
        public bool Alt { get; set; }
        public bool Win { get; set; }
        public bool Shift { get; set; }
    }
}
