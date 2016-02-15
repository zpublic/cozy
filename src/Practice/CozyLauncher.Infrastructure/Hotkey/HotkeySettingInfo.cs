using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public class HotkeySettingInfo
    {
        public Dictionary<string, HotkeyModel> HotkeyList { get; set; }
        public bool ReplaceWinR { get; set; }
    }
}
