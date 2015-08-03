using CozyAnywhere.Plugin.WinKeyboard.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;
using PluginHelper;

namespace CozyAnywhere.Plugin.WinKeyboard
{
    public partial class KeyboardPlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            var asm         = Assembly.GetExecutingAssembly();
            var factorylist = ArgsFactoryLoader.LoadArgsFactory(asm, "CozyAnywhere.Plugin.WinKeyboard.ArgsFactory");

            foreach (var obj in factorylist)
            {
                var factory                 = (IPluginCommandMethodArgsFactory)Activator.CreateInstance(obj.Item2);
                MethodDictionary[obj.Item1] = factory;
            }
        }

        public static string MakeKeyboardEventCommand(VirtualKey key, byte scan, uint flag, uint extraInfo)
        {
            var args = new KeyboardEventArgs()
            {
                Key         = key,
                ScanKey     = scan,
                Flag        = flag,
                ExtraInfo   = extraInfo,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "KeyboardEvent", argsSerialize);
        }

        public static string MakeKeyboardQueryKeyStateCommand(VirtualKey key)
        {
            var args = new KeyboardQueryKeyStateArgs()
            {
                Key         = key,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "KeyboardQueryKeyState", argsSerialize);
        }

        public static string MakeKeyboardSendKeyEventCommand(VirtualKey key)
        {
            var args = new KeyboardSendKeyEventArgs()
            {
                Key         = key,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "KeyboardSendKeyEvent", argsSerialize);
        }
    }
}