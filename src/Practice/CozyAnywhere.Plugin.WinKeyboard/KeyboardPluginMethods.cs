using CozyAnywhere.Plugin.WinKeyboard.Args;
using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System;

namespace CozyAnywhere.Plugin.WinKeyboard
{
    public partial class KeyboardPlugin
    {
        public PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public PluginMethodReturnValueType Shell(KeyboardEventArgs args)
        {
            KeyboardUtil.KeyboardEvent(args.Key, args.ScanKey, args.Flag, args.ExtraInfo);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(KeyboardQueryKeyStateArgs args)
        {
            var result = KeyboardUtil.QueryKeyState(args.Key);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(KeyboardSendKeyEventArgs args)
        {
            KeyboardUtil.SendKeyEvent(args.Key);
            return null;
        }
    }
}