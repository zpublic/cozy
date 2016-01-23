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
        private Dictionary<string, ICommand> RegistedHotkeyCommand { get; set; }
            = new Dictionary<string, ICommand>();

        public void RegistHotkey(string hotkeyName, HotkeyModel keyModel)
        {
            RegistedHotKey[hotkeyName] = keyModel;
        }

        public void RegistHotkeyAction(string hotkeyName, ICommand command)
        {
            RegistedHotkeyCommand[hotkeyName] = command;
        }

        public HotkeyModel GetRegistedHotkey(string hotkeyName)
        {
            if(RegistedHotKey.ContainsKey(hotkeyName))
            {
                return RegistedHotKey[hotkeyName];
            }
            return null;
        }

        public void InvokeHotkeyCommand(string hotkeyName)
        {
            if(RegistedHotkeyCommand.ContainsKey(hotkeyName))
            {
                var command = RegistedHotkeyCommand[hotkeyName];
                if(command != null && command.CanExecute(null))
                {
                    command.Execute(null);
                }
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
