using CozyAnywhere.Plugin.WinMouse.Args;
using CozyAnywhere.Plugin.WinMouse.ArgsFactory;
using CozyAnywhere.Plugin.WinMouse.Tag;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinMouse
{
    public partial class MousePlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            MethodDictionary["MouseClick"]                  = new MouseClickArgsFactory();
            MethodDictionary["MouseCursorClip"]             = new MouseCursorClipArgsFactory();
            MethodDictionary["MouseCursorUnClip"]           = new MouseCursorUnClipArgsFactory();
            MethodDictionary["MouseEvent"]                  = new MouseEventArgsFactory();
            MethodDictionary["MouseGetCurrsorPosition"]     = new MouseGetCurrsorPositionArgsFactory();
            MethodDictionary["MouseLeftClick"]              = new MouseLeftClickArgsFactory();
            MethodDictionary["MouseMiddleClick"]            = new MouseMiddleClickArgsFactory();
            MethodDictionary["MouseRightClick"]             = new MouseRightClickArgsFactory();
            MethodDictionary["MouseSetCursorPosition"]      = new MouseSetCursorPositionArgsFactory();
        }

        public static string MakeMouseClickCommand(ButtonTag tag, uint x, uint y)
        {
            var args = new MouseClickArgs()
            {
                Tag = tag,
                X   = x,
                Y   = y,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseClick", argsSerialize);
        }

        public static string MakeMouseCursorClipCommand(int left, int top, int right, int bottom)
        {
            var args = new MouseCursorClipArgs()
            {
                Left    = left,
                Top     = top,
                Right   = right,
                Bottom  = bottom,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseCursorClip", argsSerialize);
        }

        public static string MakeMouseCursorUnClipCommand()
        {
            var args = new MouseCursorUnClipArgs();
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseCursorUnClip", argsSerialize);
        }

        public static string MakeMouseEventCommand(MouseEventTag tag, uint x, uint y, uint data, uint extinfo)
        {
            var args = new MouseEventArgs()
            {
                Tag     = tag,
                X       = x,
                Y       = y,
                Data    = data,
                ExtInfo = extinfo,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseEvent", argsSerialize);
        }

        public static string MakeMouseGetCurrsorPositionCommand()
        {
            var args = new MouseGetCurrsorPositionArgs();
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseGetCurrsorPosition", argsSerialize);
        }

        public static string MakeMouseLeftClickCommand(uint x, uint y)
        {
            var args = new MouseLeftClickArgs()
            {
                X = x,
                Y = y,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseLeftClick", argsSerialize);
        }

        public static string MakeMouseMiddleClickCommand(uint x, uint y)
        {
            var args = new MouseMiddleClickArgs()
            {
                X = x,
                Y = y,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseMiddleClick", argsSerialize);
        }

        public static string MakeMouseRightClickCommand(uint x, uint y)
        {
            var args = new MouseRightClickArgs()
            {
                X = x,
                Y = y,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseRightClick", argsSerialize);
        }

        public static string MakeMouseSetCursorPositionCommand(int x, int y)
        {
            var args = new MouseSetCursorPositionArgs()
            {
                X = x,
                Y = y,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "MouseSetCursorPosition", argsSerialize);
        }
    }
}