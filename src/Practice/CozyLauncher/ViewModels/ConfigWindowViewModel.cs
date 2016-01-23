using CozyLauncher.Commands;
using CozyLauncher.Infrastructure.Hotkey;
using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _SubmitCommand = _SubmitCommand ?? new DelegateCommand(x =>
                {
                    if (!string.IsNullOrEmpty(HotkeyTextStr))
                    {
                        var hkm = new HotkeyModel(HotkeyTextStr);
                        if (hkm.CharKey != Key.None)
                        {
                            GlobalHotkey.Instance.RegistHotkey("HotKey.ShowApp", hkm);
                            HotkeyManager.Current.AddOrReplace("HotKey.ShowApp", hkm.CharKey, hkm.ModifierKeyStatus, (s, ee) =>
                            {
                                GlobalHotkey.Instance.InvokeHotkeyCommand("HotKey.ShowApp");
                            });
                        }
                    }
                });
            }
        }



        public ConfigWindowViewModel()
        {
            var hkm1 = GlobalHotkey.Instance.GetRegistedHotkey("HotKey.ShowApp");
            if (hkm1 != null)
            {
                HotkeyTextStr = hkm1.ToString();
            }
        }
    }
}
