using CozyLauncher.Commands;
using CozyLauncher.Infrastructure.Hotkey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CozyLauncher.Infrastructure.StartUp;
using CozyLauncher.Core;

namespace CozyLauncher.ViewModels
{
    public class ConfigWindowViewModel : BaseViewModel
    {
        private string _HotkeyTextStr;
        public string HotkeyTextStr
        {
            get
            {
                return _HotkeyTextStr;
            }
            set
            {
                Set(ref _HotkeyTextStr, value);
            }
        }

        private bool _ReplaceWinR;
        public bool ReplaceWinR
        {
            get
            {
                return _ReplaceWinR;
            }
            set
            {
                Set(ref _ReplaceWinR, value);
            }
        }

        private bool _AutoStartUp;
        public bool AutoStartUp
        {
            get
            {
                return _AutoStartUp;
            }
            set
            {
                _AutoStartUp = value;
            }
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _SubmitCommand = _SubmitCommand ?? new DelegateCommand(x =>
                {
                    StartUpManager.Instance.IsAutoStartUp = AutoStartUp;

                    GlobalHotkey.Instance.ReplaceWindowR = ReplaceWinR;

                    if (!string.IsNullOrEmpty(HotkeyTextStr))
                    {
                        try
                        {
                            GlobalHotkey.Instance.RegistHotkey("HotKey.ShowApp", new HotkeyModel(HotkeyTextStr));
                            SaveConfig();
                        }
                        catch(Exception)
                        {
                            // failed
                        }
                    }
                });
            }
        }

        private void SaveConfig()
        {
            var hkm = GlobalHotkey.Instance.GetRegistedHotkey("HotKey.ShowApp");

            SettingObject.Instance.Set("Hotkey", "IsCtrl", hkm.Ctrl);
            SettingObject.Instance.Set("Hotkey", "IsShift", hkm.Shift);
            SettingObject.Instance.Set("Hotkey", "IsAlt", hkm.Alt);
            SettingObject.Instance.Set("Hotkey", "IsWin", hkm.Win);
            SettingObject.Instance.Set("Hotkey", "Key", hkm.CharKey);

            SettingObject.Instance.Set("Hotkey", "ReplaceWindowR", GlobalHotkey.Instance.ReplaceWindowR);

            SettingObject.Instance.Save();
        }

        public ConfigWindowViewModel()
        {
            var hkm1 = GlobalHotkey.Instance.GetRegistedHotkey("HotKey.ShowApp");
            if (hkm1 != null)
            {
                HotkeyTextStr = hkm1.ToString();
            }
            ReplaceWinR = GlobalHotkey.Instance.ReplaceWindowR;
            AutoStartUp = StartUpManager.Instance.IsAutoStartUp;
        }
    }
}
