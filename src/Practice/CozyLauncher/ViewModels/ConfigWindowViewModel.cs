using CozyLauncher.Commands;
using CozyLauncher.Infrastructure.Hotkey;
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
                        GlobalHotkey.Instance.RegistHotkey("HotKey.ShowApp", new HotkeyModel(HotkeyTextStr));
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
