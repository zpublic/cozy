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
        private string _HotkeyText;
        public string HotkeyText
        {
            get
            {
                return _HotkeyText;
            }
            set
            {
                Set(ref _HotkeyText, value);
            }
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _SubmitCommand = _SubmitCommand ?? new DelegateCommand(x =>
                {
                    if (!string.IsNullOrEmpty(HotkeyText))
                    {
                        var hkm = new HotkeyModel(HotkeyText);
                        if (hkm.CharKey != Key.None)
                        {
                            GlobalHotkey.Instance.RegistHotkey("HotKey.ShowApp", hkm);
                            HotkeyManager.Current.AddOrReplace("HotKey.ShowApp", hkm.CharKey, hkm.ModifierKeyStatus, (s, ee) =>
                            {
                                GlobalHotkey.Instance.InvokeHotkeyAction("HotKey.ShowApp");
                            });
                        }
                    }
                });
            }
        }
    }
}
