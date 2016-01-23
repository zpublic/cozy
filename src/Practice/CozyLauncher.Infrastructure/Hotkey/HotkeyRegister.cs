using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozyLauncher.Infrastructure.Hotkey
{
    public static class HotkeyRegister
    {
        public static void Regist(string name, HotkeyModel keyModel, Action action)
        {
            if (keyModel.CharKey != Key.None)
            {
                HotkeyManager.Current.AddOrReplace(name, keyModel.CharKey, keyModel.ModifierKeyStatus, (s, ee) =>
                {
                    action?.Invoke();
                });
            }
        }

        public static void UnRegist(string name)
        {
            HotkeyManager.Current.Remove(name);
        }
    }
}
