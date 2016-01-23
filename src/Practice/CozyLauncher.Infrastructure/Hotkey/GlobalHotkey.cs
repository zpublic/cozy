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
        public Action<HotkeyModel> AddHotKeyDelegate { get; set; }

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

        private Dictionary<string, HotkeyModel> RegistedCommandHotkey { get; set; }
            = new Dictionary<string, HotkeyModel>();
        private Dictionary<string, string> RegistedCommand { get; set; }
            = new Dictionary<string, string>();

        public void RegistHotkey(string hotkeyName, HotkeyModel keyModel)
        {
            RegistedHotKey[hotkeyName] = keyModel;
        }

        public void RegistHotkeyAction(string hotkeyName, Action action)
        {
            RegistedHotkeyAction[hotkeyName] = action;
        }

        public void RegistCommandHotkey(string hotkeyName, HotkeyModel keyModel, string hotkeyCommand)
        {
            RegistedCommandHotkey[hotkeyName] = keyModel;
            RegistedCommand[hotkeyName] = hotkeyCommand;
        }

        public HotkeyModel GetRegistedHotkey(string hotkeyName)
        {
            if(RegistedHotKey.ContainsKey(hotkeyName))
            {
                return RegistedHotKey[hotkeyName];
            }
            return null;
        }

        public HotkeyModel GetRegistedCommandHotkey(string hotkeyName)
        {
            if (RegistedCommandHotkey.ContainsKey(hotkeyName))
            {
                return RegistedCommandHotkey[hotkeyName];
            }
            return null;
        }

        public string GetRegistedCommand(string hotkeyName)
        {
            if (RegistedCommand.ContainsKey(hotkeyName))
            {
                return RegistedCommand[hotkeyName];
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
