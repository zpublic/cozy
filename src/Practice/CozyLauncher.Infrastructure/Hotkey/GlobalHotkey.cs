using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;

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

        public void RegistHotkey(string hotkeyName, HotkeyModel keyModel)
        {
            RegistedHotKey[hotkeyName] = keyModel;

            HotkeyRegister.Regist(hotkeyName, keyModel, () =>
            {
                InvokeHotkeyAction(hotkeyName);
            });
        }

        private void InvokeHotkeyAction(string hotkeyName)
        {
            if (RegistedHotkeyAction.ContainsKey(hotkeyName))
            {
                RegistedHotkeyAction[hotkeyName]?.Invoke();
            }
        }

        public void RegistHotkeyAction(string hotkeyName, Action action)
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

        public static string ConfigFilePath { get { return @"./config.json"; } }

        public void Save()
        {
            try
            {
                using (var fs = new FileStream(ConfigFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        string result = JsonConvert.SerializeObject(RegistedHotKey); ;
                        if (!string.IsNullOrEmpty(result))
                        {
                            sw.Write(result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Do something
            }
        }

        public void Load()
        {
            string result = null;

            try
            {
                using (var fs = new FileStream(ConfigFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            catch(Exception)
            {

            }

            if (!string.IsNullOrEmpty(result))
            {
                var loadData = JsonConvert.DeserializeObject<Dictionary<string, HotkeyModel>>(result);
                foreach (var obj in loadData)
                {
                    RegistHotkey(obj.Key, obj.Value);
                }
            }
            else
            {
                RegistHotkey("HotKey.ShowApp", new HotkeyModel("Ctrl+Alt+Space"));
                Save();
            }
        }
    }
}
