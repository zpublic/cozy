using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Concurrent;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public class GlobalHotkey
    {
        private static GlobalHotkey _Instance = new GlobalHotkey();
        public static GlobalHotkey Instance
        {
            get
            {
                return _Instance;
            }
        }

        public ModifyKeyStatus ModifyKeyStatus
        {
            get
            {
                ModifyKeyStatus result = new ModifyKeyStatus();
                if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None)
                {
                    result.Ctrl = true;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Alt) != ModifierKeys.None)
                {
                    result.Alt = true;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.None)
                {
                    result.Shift = true;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Windows) != ModifierKeys.None)
                {
                    result.Win = true;
                }
                return result;
            }
        }

        private Dictionary<string, HotkeyModel> RegistedHotKey { get; set; }
            = new Dictionary<string, HotkeyModel>();
        private Dictionary<string, Action> RegistedHotkeyAction { get; set; }
            = new Dictionary<string, Action>();

        public void RegisterHotkey(string hotkeyName, HotkeyModel keyModel, Action action = null)
        {
            RegistedHotKey[hotkeyName] = keyModel;
            RegisterHotkeyAction(hotkeyName, action);
        }

        public void RegisterHotkeyAction(string hotkeyName, Action action)
        {
            RegistedHotkeyAction[hotkeyName] = action;
        }

        public HotkeyModel GetRegistedHotkey(string hotkeyName)
        {
            if(RegistedHotKey.ContainsKey(hotkeyName))
            {
                return RegistedHotKey[hotkeyName];
            }
            return null;
        }

        public void InvokeHotkeyAction(string hotkeyName)
        {
            if(RegistedHotkeyAction.ContainsKey(hotkeyName))
            {
                var act = RegistedHotkeyAction[hotkeyName];
                act?.Invoke();
            }
        }

        public void Save()
        {

        }

        public void Load()
        {

        }
    }
}
