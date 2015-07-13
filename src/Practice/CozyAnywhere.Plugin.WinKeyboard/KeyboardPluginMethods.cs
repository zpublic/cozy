using CozyAnywhere.Plugin.WinKeyboard.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System;

namespace CozyAnywhere.Plugin.WinKeyboard
{
    public partial class KeyboardPlugin
    {
        public string Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public string Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public string Shell(KeyboardEventArgs args)
        {
            KeyboardUtil.KeyboardEvent(args.Key, args.ScanKey, args.Flag, args.ExtraInfo);
            return null;
        }

        public string Shell(KeyboardQueryKeyStateArgs args)
        {
            var result = KeyboardUtil.QueryKeyState(args.Key);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(KeyboardSendKeyEventArgs args)
        {
            KeyboardUtil.SendKeyEvent(args.Key);
            return null;
        }
    }
}