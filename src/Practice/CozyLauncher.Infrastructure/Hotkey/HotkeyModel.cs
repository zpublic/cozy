using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public class HotkeyModel
    {
        public bool Ctrl { get; set; }
        public bool Alt { get; set; }
        public bool Win { get; set; }
        public bool Shift { get; set; }
        public Key CharKey { get; set; }

        HotkeyModel()
        {

        }

        public HotkeyModel(string hotKeyString)
        {
            Parse(hotKeyString);
        }

        public HotkeyModel(ModifyKeyStatus status, Key key)
            :this(status.Ctrl, status.Shift, status.Alt, status.Win, key)
        {

        }

        public HotkeyModel(bool ctrl, bool shift, bool alt, bool win, Key key)
        {
            Ctrl    = ctrl;
            Shift   = shift;
            Alt     = alt;
            Win     = win;
            CharKey = key;
        }

        public ModifierKeys ModifierKeyStatus
        {
            get
            {
                ModifierKeys keys = ModifierKeys.None;
                if(Alt)
                {
                    keys = ModifierKeys.Alt;
                }
                if(Shift)
                {
                    keys |= ModifierKeys.Shift;
                }
                if (Ctrl)
                {
                    keys |= ModifierKeys.Control;
                }
                if(Win)
                {
                    keys |= ModifierKeys.Windows;
                }
                return keys;
            }
        }

        private void Parse(string hotkeyString)
        {
            List<string> keyStrs = hotkeyString.Replace(" ", "").Split('+').ToList();
            if(keyStrs.Contains("Ctrl"))
            {
                Ctrl = true;
                keyStrs.Remove("Ctrl");
            }
            if(keyStrs.Contains("Shift"))
            {
                Shift = true;
                keyStrs.Remove("Shift");
            }
            if(keyStrs.Contains("Alt"))
            {
                Alt = true;
                keyStrs.Remove("Alt");
            }
            if(keyStrs.Contains("Win"))
            {
                Win = true;
                keyStrs.Remove("Win");
            }
            if(keyStrs.Count > 0)
            {
                string keyStr = keyStrs[0];
                if(keyStr == "Space")
                {
                    CharKey = Key.Space;
                }
                else
                {
                    Key ResKey = Key.None;
                    if(Enum.TryParse(keyStr, out ResKey))
                    {
                        CharKey = ResKey;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(Ctrl)
            {
                sb.Append("Ctrl + ");
            }
            if(Shift)
            {
                sb.Append("Shift + ");
            }
            if(Alt)
            {
                sb.Append("Alt + ");
            }
            if(Win)
            {
                sb.Append("Win + ");
            }
            if(CharKey != Key.None)
            {
                if(CharKey == Key.Space)
                {
                    sb.Append("Space");
                }
                else
                {
                    sb.Append(CharKey.ToString());
                }
            }
            else
            {
                sb.Remove(sb.Length - 3, 3);
            }
            return sb.ToString();
        }
    }
}
