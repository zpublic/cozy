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

        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_ALT = 0x12;
        private const int VK_WIN = 91;

        public ModifyKeyStatus ModifyKeyStatus
        {
            get
            {
                ModifyKeyStatus result = new ModifyKeyStatus();
                if ((HotkeyNative.GetKeyState(VK_CONTROL) & 0x8000) != 0)
                {
                    result.Ctrl = true;
                }
                if ((HotkeyNative.GetKeyState(VK_ALT) & 0x8000) != 0)
                {
                    result.Alt = true;
                }
                if ((HotkeyNative.GetKeyState(VK_SHIFT) & 0x8000) != 0)
                {
                    result.Shift = true;
                }
                if ((HotkeyNative.GetKeyState(VK_WIN) & 0x8000) != 0)
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

        public void Init()
        {
            HotkeyNative.Init();
        }

        public void Release()
        {
            HotkeyNative.Release();
            UnregistAllHotkey();
        }

        public void RegistHotkey(string hotkeyName, HotkeyModel keyModel)
        {
            RegistedHotKey[hotkeyName] = keyModel;

            try
            {
                HotkeyRegister.Regist(hotkeyName, keyModel, () =>
                {
                    InvokeHotkeyAction(hotkeyName);
                });
            }
            catch (NHotkey.HotkeyAlreadyRegisteredException)
            {
                RegistedHotKey.Remove(hotkeyName);
                throw new Exception("Register failed");
            }
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

        public void UnregistHotkey(string hotkeyName)
        {
            if(RegistedHotKey.ContainsKey(hotkeyName))
            {
                RegistedHotKey.Remove(hotkeyName);
                HotkeyRegister.UnRegist(hotkeyName);
            }
            if(RegistedHotkeyAction.ContainsKey(hotkeyName))
            {
                RegistedHotkeyAction.Remove(hotkeyName);
            }
        }

        public void UnregistAllHotkey()
        {
            foreach(var obj in RegistedHotKey)
            {
                HotkeyRegister.UnRegist(obj.Key);
            }
            RegistedHotKey.Clear();
            RegistedHotkeyAction.Clear();
        }

        private static Func<int, int, bool> ReplaceWindowRProcAction { get; set; }

        private bool _ReplaceWindowR;
        public bool ReplaceWindowR
        {
            get
            {
                return _ReplaceWindowR;
            }
            set
            {
                if(_ReplaceWindowR != value)
                {
                    _ReplaceWindowR = value;
                    if(ReplaceWindowR)
                    {
                        ReplaceWindowRProcAction = new Func<int, int, bool>(ReplaceWindowRProc);
                        HotkeyNative.ProcessCallback = ReplaceWindowRProcAction;
                    }
                    else
                    {
                        HotkeyNative.ProcessCallback = null;
                    }
                }
            }
        }
        // "HotKey.ShowApp"
        public Action ReplaceWindowRAction;

        private bool ReplaceWindowRProc(int vkey, int scankey)
        {
            if(ReplaceWindowR)
            {
                if (ModifyKeyStatus.Win && KeyInterop.KeyFromVirtualKey(vkey) == Key.R)
                {
                    ReplaceWindowRAction?.Invoke();
                    return true;
                }
            }
            return false;
        }
    }
}
